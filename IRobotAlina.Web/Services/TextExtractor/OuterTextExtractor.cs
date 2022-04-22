using Hangfire;
using IRobotAlina.Data.Entities;
using IRobotAlina.Web.BackgroundJob;
using System.IO;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.TextExtractor
{
    public class OuterTextExtractor :
        IPdfDocumentTextExtractor, IImageDocumentTextExtractor,
        IDocDocumentTextExtractor, IDocXDocumentTextExtractor,
        IXlsDocumentTextExtractor, IXlsxDocumentTextExtractor
    {
        public Task Extract(IAttachment attachment)                
        {
           
            var extension = Path.GetExtension(attachment.FileName).ToLowerInvariant();

            switch (extension)
            {
                case ".pdf":
                    Hangfire.BackgroundJob.Enqueue<OuterTextExtractionService>(x => x.ExecutePdf(attachment.Id));
                    break;

                case ".jpg":
                case ".jpeg":
                    Hangfire.BackgroundJob.Enqueue<OuterTextExtractionService>(x => x.ExecuteJpg(attachment.Id));
                    break;

                default:
                    Hangfire.BackgroundJob.Enqueue<OuterTextExtractionService>(x => x.Execute(attachment.Id));
                    break;
            }
                                    
            return Task.CompletedTask;
        }
    }
}
