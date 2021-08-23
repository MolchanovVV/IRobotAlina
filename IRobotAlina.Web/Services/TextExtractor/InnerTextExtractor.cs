using IRobotAlina.Data.Entities;
using NamedPipeWrapper;
using System.Threading.Tasks;


namespace IRobotAlina.Web.Services.TextExtractor
{
    public class InnerTextExtractor : ITxtDocumentTextExtractor
    {
        public Task Extract(IAttachment attachment)
        {
            attachment.Status = ETenderFileAttachmentStatus.InProgress;

            Hangfire.BackgroundJob.Enqueue<InnerTextExtractionService>(x => x.Execute(attachment.Id));
            
            return Task.CompletedTask;
        }
    }
}
