using IRobotAlina.Data.Entities;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.TextExtractor
{
    public class OuterTextExtractor :
        IPdfDocumentTextExtractor, IImageDocumentTextExtractor,
        IDocDocumentTextExtractor, IDocXDocumentTextExtractor,
        IXlsDocumentTextExtractor, IXlsxDocumentTextExtractor
    {
        private readonly IOuterTextExtractionService outerTextExtractionService;

        public OuterTextExtractor(IOuterTextExtractionService outerTextExtractionService)
        {
            this.outerTextExtractionService = outerTextExtractionService;
        }

        public async Task Extract(IAttachment attachment)
        {            
            await Task.Run(() => outerTextExtractionService.TextExtract(attachment.Id));         
        }        
    }
}
