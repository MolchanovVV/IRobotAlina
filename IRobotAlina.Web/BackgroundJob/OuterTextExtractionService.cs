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

        [Queue("beta")]
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
    }
}
