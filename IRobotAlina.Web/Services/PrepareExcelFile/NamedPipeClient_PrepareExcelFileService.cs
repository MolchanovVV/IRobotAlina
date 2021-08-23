using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NamedPipeWrapper;
using System;
using System.Threading.Tasks;


namespace IRobotAlina.Web.Services.PrepareExcelFile
{
    public class NamedPipeClient_PrepareExcelFileService
    {
        private readonly ILogger logger;

        private NamedPipeClient<DataMessage> namedPipeClient;
        private Exl_DataMessage dataMessage;
        private int mailId;
        private string fileName;
        private byte[] content;

        public NamedPipeClient_PrepareExcelFileService(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            logger = scope.ServiceProvider.GetService<ILogger<NamedPipeClient_PrepareExcelFileService>>();
        }

        public async Task<Exl_DataMessage> SendRequestToPrepareExcelFile(int mailId, string fileName, byte[] content)
        {
            this.mailId = mailId;
            this.fileName = fileName;
            this.content = new byte[content.Length];
            content.CopyTo(this.content, 0);

            try
            {
                namedPipeClient = new NamedPipeClient<DataMessage>(Constants.PIPE_NAME, applicationName: "IRobotAlina") { AutoReconnect = false };
                namedPipeClient.ServerMessage += OnServerMessage;
                namedPipeClient.Error += OnError;
                namedPipeClient.Start();
                namedPipeClient.WaitForDisconnection(TimeSpan.FromMinutes(2));

                if (dataMessage is null || (dataMessage.type != DataMessageSettings.MessageType.Exl_Response && dataMessage.type != DataMessageSettings.MessageType.Error))
                {
                    dataMessage = new Exl_DataMessage()
                    {
                        type = DataMessageSettings.MessageType.Error,
                        errMsg = "Превышено время ожидания ответа от сервера."
                    };
                }

                return await Task.FromResult(dataMessage);
            }
            finally
            {
                this.content = null;
                namedPipeClient?.Stop();
            }
        }

        private void OnServerMessage(NamedPipeConnection<DataMessage, DataMessage> connection, DataMessage message)
        {
            try
            {
                if (message.type == DataMessageSettings.MessageType.Init) // здесь message приходит как DataMessage
                {
                    dataMessage = new Exl_DataMessage
                    {
                        type = DataMessageSettings.MessageType.Exl_Request,
                        mailId = this.mailId,
                        fileName = this.fileName,
                        content = this.content
                    };

                    connection.PushMessage(dataMessage);
                }
                else
                {
                    if (!(message is Exl_DataMessage exl_message))
                        throw new Exception($"Неизвестный класс сообщения, [{message.GetType().Name}].");

                    switch (exl_message.type)
                    {
                        case DataMessageSettings.MessageType.Exl_Response:
                        case DataMessageSettings.MessageType.Error:
                            dataMessage.type = exl_message.type;
                            dataMessage.content = exl_message.content;
                            dataMessage.errMsg = exl_message.errMsg;

                            namedPipeClient.Stop();
                            namedPipeClient = null;
                            break;

                        default:
                            string errMessage;
                            if (Enum.IsDefined(typeof(DataMessageSettings.MessageType), exl_message.type))
                                errMessage = $"Неожиданный тип сообщения: [{DataMessageSettings.TypeStr[exl_message.type]}]";
                            else
                                errMessage = "Неизвестный тип сообщения.";

                            throw new Exception(errMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (dataMessage != null)
                    dataMessage = new Exl_DataMessage();

                dataMessage.type = DataMessageSettings.MessageType.Error;
                dataMessage.errMsg = ex.Message;
                dataMessage.content = null;

                logger.LogError(ex, $"PrepareExcelFile.SendRequestToPrepareExcelFile, mailId [{mailId}], file [\"{fileName}\"].");
                ex.Data.Add("methodName", "NamedPipeClient_OnServerMessage");
                OnError(ex);
            }
        }

        private void OnError(Exception ex)
        {
            if (!ex.Data.Contains("methodName"))
                logger.LogError(ex, $"NamedPipeClient_OnError");

            this.content = null;
            namedPipeClient?.Stop();
            namedPipeClient = null;
        }
    }
}
