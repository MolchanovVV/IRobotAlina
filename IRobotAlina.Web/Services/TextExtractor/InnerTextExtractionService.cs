using Hangfire;
using System;
using System.Linq;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.TextExtractor
{
    public class InnerTextExtractionService : IDisposable
    {
        private readonly ApplicationDbContext context;
        
        public InnerTextExtractionService(ApplicationDbContext context)
        {
            this.context = context;            
        }

        public void Dispose()
        {
            context.Dispose();
        }

        [Queue("main")]
        public async Task Execute(Guid id)
        {
            var tenderAttachment = context.TenderFileAttachments.FirstOrDefault(x => x.Id == id);

            if (tenderAttachment == null)
                return;

            try
            {
                tenderAttachment.ExtractedText = await Task.Run(() => System.Text.Encoding.UTF8.GetString(tenderAttachment.Content));
                tenderAttachment.ExceptionMessage = null;
                tenderAttachment.Status = Data.Entities.ETenderFileAttachmentStatus.Success;
            }
            catch (Exception ex)
            {
                tenderAttachment.ExtractedText = null;
                tenderAttachment.ExceptionMessage = ex.Message;
                tenderAttachment.Status = Data.Entities.ETenderFileAttachmentStatus.Error;
            }
            finally
            {
                context.Update(tenderAttachment);
                context.SaveChanges();
            }
        }
    }
}
