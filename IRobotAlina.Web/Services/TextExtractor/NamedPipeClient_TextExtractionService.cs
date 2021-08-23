using IRobotAlina.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NamedPipeWrapper;
using System.Threading;

namespace IRobotAlina.Web.Services.TextExtractor
{    
    public class NamedPipeClient_TextExtractionService
    {        
        private readonly ILogger logger;

        private NamedPipeClient<DataMessage> namedPipeClient;              
        private TE_DataMessage dataMessage;
        private int attachmentTenderId;
        private string attachmentFileName;
        private byte[] attachmentContent;

        public NamedPipeClient_TextExtractionService(IServiceProvider serviceProvider)
        {            
            using var scope = serviceProvider.CreateScope();
            logger = scope.ServiceProvider.GetService<ILogger<NamedPipeClient_TextExtractionService>>();
        }
        
        public async Task<TE_DataMessage> SendRequestToTextExtract(IAttachment attachment)
        {            
            attachmentTenderId = attachment.TenderId;
            attachmentFileName = attachment.FileName;
            attachmentContent = new byte[attachment.Content.Length];
            attachment.Content.CopyTo(attachmentContent, 0);

            try
            {
                namedPipeClient = new NamedPipeClient<DataMessage>(Constants.PIPE_NAME, applicationName: "IRobotAlina") { AutoReconnect = false };
                namedPipeClient.ServerMessage += OnServerMessage;
                namedPipeClient.Error += OnError;
                namedPipeClient.Start();
                namedPipeClient.WaitForDisconnection(TimeSpan.FromMinutes(20));
                
                if (dataMessage is null || (dataMessage.type != DataMessageSettings.MessageType.TE_Response && dataMessage.type != DataMessageSettings.MessageType.Error))
                {
                    dataMessage = new TE_DataMessage
                    {
                        type = DataMessageSettings.MessageType.Error,                        
                        errMsg = "Превышено время ожидания ответа от сервера."
                    };
                }
                
                return await Task.FromResult(dataMessage);
            }            
            finally
            {
                attachmentContent = null;
                namedPipeClient?.Stop();
            }
        }

        private void OnServerMessage(NamedPipeConnection<DataMessage, DataMessage> connection, DataMessage message)
        {
            try
            {                
                if (message.type == DataMessageSettings.MessageType.Init) // здесь message приходит как DataMessage
                {
                    dataMessage = new TE_DataMessage
                    {
                        type = DataMessageSettings.MessageType.TE_Request,
                        tenderId = attachmentTenderId,
                        fileName = attachmentFileName,
                        content = attachmentContent
                    };

                    connection.PushMessage(dataMessage);
                }
                else // и здесь мы уже работаем обязательно как с TE_DataMessage
                {
                    if (!(message is TE_DataMessage te_message))
                        throw new Exception($"Некорректный класс сообщения, [{message.GetType().Name}].");

                    switch (te_message.type)
                    {
                        case DataMessageSettings.MessageType.TE_Rejection:
                            Thread.Sleep(new Random().Next(10000, 30000));
                            dataMessage.type = DataMessageSettings.MessageType.TE_Request;                           
                            connection.PushMessage(dataMessage);
                            break;

                        case DataMessageSettings.MessageType.TE_Response:
                        case DataMessageSettings.MessageType.Error:                            
                            dataMessage.type = te_message.type;                                         
                            dataMessage.content = null;
                            dataMessage.serviceResult = te_message.serviceResult;
                            dataMessage.errMsg = te_message.errMsg;
               
                            namedPipeClient.Stop();
                            namedPipeClient = null;
                            break;

                        default:
                            string errMessage;
                            if (Enum.IsDefined(typeof(DataMessageSettings.MessageType), te_message.type))
                                errMessage = $"Неожиданный тип сообщения: [{DataMessageSettings.TypeStr[te_message.type]}]";
                            else
                                errMessage = "Неизвестный тип сообщения.";

                            throw new Exception(errMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (dataMessage != null)
                    dataMessage = new TE_DataMessage();
                
                dataMessage.type = DataMessageSettings.MessageType.Error;
                dataMessage.content = null;
                dataMessage.serviceResult = null;
                dataMessage.errMsg = ex.Message;
                                                                
                logger.LogError(ex, $"TextExtractor.SendRequestToTextExtract, tenderId [{attachmentTenderId}],  file [\"{attachmentFileName}\"]");
                ex.Data.Add("methodName", "NamedPipeClient_OnServerMessage");
                OnError(ex);
            }
        }

        private void OnError(Exception ex)
        {

            if (!ex.Data.Contains("methodName"))
                logger.LogError(ex, $"NamedPipeClient_OnError");

            attachmentContent = null;
            namedPipeClient?.Stop();
        }
    }
}
