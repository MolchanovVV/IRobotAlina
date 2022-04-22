using Hangfire;
using Hangfire.States;
using IRobotAlina.Web.Services.Storage;
using IRobotAlina.Web.Services.TextExtractor;
using NamedPipeWrapper;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.BackgroundJob
{
    [AutomaticRetry(Attempts = 2)]
    public class OuterTextExtractionService : IOuterTextExtractionService, IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private readonly NamedPipeClient_TextExtractionService textExtractionService;
        private readonly ISaveTenderFileAttachment saveTenderFileToDatabaseService;

        public OuterTextExtractionService(
            ApplicationDbContext dbContext,
            NamedPipeClient_TextExtractionService textExtractionService,
            ISaveTenderFileAttachment saveTenderFileToDatabaseService)
        {
            this.dbContext = dbContext;
            this.textExtractionService = textExtractionService;
            this.saveTenderFileToDatabaseService = saveTenderFileToDatabaseService;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async Task TextExtract(Guid id)
        {
            var tenderAttachment = dbContext.TenderFileAttachments.FirstOrDefault(x => x.Id == id);

            if (tenderAttachment == null)
                return;

            string queueName, extension = Path.GetExtension(tenderAttachment.FileName).ToLowerInvariant();

            switch (extension)
            {
                case ".pdf":
                    queueName = "pdf";
                    break;

                case ".jpg":
                case ".jpeg":
                    queueName = "jpg";
                    break;

                default:
                    queueName = "main";
                    break;
            }

            var client = new BackgroundJobClient();
            await Task.Run(() => client.Create(() => Execute(tenderAttachment), new EnqueuedState(queueName)));
        }
        
        public async Task Execute(Data.Entities.TenderFileAttachment tenderAttachment)        
        {            
            TE_DataMessage result;
            try
            {
                result = await textExtractionService.SendRequestToTextExtract(tenderAttachment);

                tenderAttachment.ExtractedText = result.serviceResult;
                tenderAttachment.ExceptionMessage = result.errMsg;
                tenderAttachment.Status = result.type == DataMessageSettings.MessageType.TE_Response ? Data.Entities.ETenderFileAttachmentStatus.Success : Data.Entities.ETenderFileAttachmentStatus.Error;
            }
            catch (Exception ex)
            {
                tenderAttachment.ExtractedText = null;
                tenderAttachment.ExceptionMessage = ex.Message;
                tenderAttachment.Status = Data.Entities.ETenderFileAttachmentStatus.Error;
            }
            finally
            {
                await saveTenderFileToDatabaseService.Update(tenderAttachment);
                result = null;
            }
        }        
    }
}
