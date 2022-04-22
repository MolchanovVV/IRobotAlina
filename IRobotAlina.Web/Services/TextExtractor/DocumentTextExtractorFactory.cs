using System.IO;

namespace IRobotAlina.Web.Services.TextExtractor
{
    public class DocumentTextExtractorFactory : IDocumentTextExtractorFactory
    {
        private readonly IPdfDocumentTextExtractor pdfDocumentTextExtractor;
        private readonly IDocDocumentTextExtractor docDocumentTextExtractor;
        private readonly IDocXDocumentTextExtractor docXDocumentTextExtractor;
        private readonly ITxtDocumentTextExtractor txtDocumentTextExtractor;
        private readonly IXlsDocumentTextExtractor xlsDocumentTextExtractor;
        private readonly IXlsxDocumentTextExtractor xlsxDocumentTextExtractor;
        private readonly IImageDocumentTextExtractor imageDocumentTextExtractor;

        public DocumentTextExtractorFactory(
            IPdfDocumentTextExtractor pdfDocumentTextExtractor,
            IDocDocumentTextExtractor docDocumentTextExtractor,
            IDocXDocumentTextExtractor docXDocumentTextExtractor,
            ITxtDocumentTextExtractor txtDocumentTextExtractor,
            IXlsDocumentTextExtractor xlsDocumentTextExtractor,
            IXlsxDocumentTextExtractor xlsxDocumentTextExtractor,
            IImageDocumentTextExtractor imageDocumentTextExtractor)
        {
            this.pdfDocumentTextExtractor = pdfDocumentTextExtractor;
            this.docDocumentTextExtractor = docDocumentTextExtractor;
            this.docXDocumentTextExtractor = docXDocumentTextExtractor;
            this.txtDocumentTextExtractor = txtDocumentTextExtractor;
            this.xlsDocumentTextExtractor = xlsDocumentTextExtractor;
            this.xlsxDocumentTextExtractor = xlsxDocumentTextExtractor;
            this.imageDocumentTextExtractor = imageDocumentTextExtractor;
        }
        
        public IDocumentTextExtractor GetInstance(string fileName)
        {          
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".pdf"  => pdfDocumentTextExtractor,
                ".doc"  => docDocumentTextExtractor,
                ".docx" => docXDocumentTextExtractor,
                ".txt"  => txtDocumentTextExtractor,
                ".xls"  => xlsDocumentTextExtractor,
                ".xlsx" => xlsxDocumentTextExtractor,                
                ".png"  => imageDocumentTextExtractor,
                ".jpg"  => imageDocumentTextExtractor,                
                ".jpeg" => imageDocumentTextExtractor,
                ".bmp"  => imageDocumentTextExtractor,
                _ => null
            };
        }        
    }
}
