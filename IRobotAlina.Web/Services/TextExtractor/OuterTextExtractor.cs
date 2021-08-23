using IRobotAlina.Data.Entities;
using IRobotAlina.Web.BackgroundJob;
using NamedPipeWrapper;
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
            Hangfire.BackgroundJob.Enqueue<OuterTextExtractionService>(x => x.Execute(attachment.Id));
            
            return Task.CompletedTask;
        }
    }
}
