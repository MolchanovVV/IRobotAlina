using Hangfire;
using IRobotAlina.Web.Services.Storage;
using IRobotAlina.Web.Services.TextExtractor;
using NamedPipeWrapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.BackgroundJob
{
    [AutomaticRetry(Attempts = 2)]
    public class OuterTextExtractionService : IDisposable
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

        //public async Task TextExtract(Guid id)
        //{
        //    var tenderAttachment = dbContext.TenderFileAttachments.FirstOrDefault(x => x.Id == id);

        //    if (tenderAttachment == null)
        //        return;

        //    string queueName, extension = Path.GetExtension(tenderAttachment.FileName).ToLowerInvariant();

        //    switch (extension)
        //    {
        //        case ".pdf":
        //            queueName = "pdf";
        //            break;

        //        case ".jpg":
        //        case ".jpeg":
        //            queueName = "jpg";
        //            break;

        //        default:
        //            queueName = "main";
        //            break;
        //    }

        //    var client = new BackgroundJobClient();
        //    await Task.Run(() => client.Create(() => Execute(tenderAttachment), new EnqueuedState(queueName)));
        //}

        ////The type `IRobotAlina.Web.BackgroundJob.OuterTextExtractionService` does not contain a method with signature `TextExtract(Guid)`

        //private async Task Execute(Data.Entities.TenderFileAttachment tenderAttachment)
        [Queue("main")]
        public async Task Execute(Guid id)
        {
            var tenderAttachment = dbContext.TenderFileAttachments.FirstOrDefault(x => x.Id == id);

            if (tenderAttachment == null)
                return;

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

        [Queue("pdf")]
        public async Task ExecutePdf(Guid id)
        {
            var tenderAttachment = dbContext.TenderFileAttachments.FirstOrDefault(x => x.Id == id);

            if (tenderAttachment == null)
                return;

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

        [Queue("jpg")]
        public async Task ExecuteJpg(Guid id)
        {
            var tenderAttachment = dbContext.TenderFileAttachments.FirstOrDefault(x => x.Id == id);

            if (tenderAttachment == null)
                return;

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
