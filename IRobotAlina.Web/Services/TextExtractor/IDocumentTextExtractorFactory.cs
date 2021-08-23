namespace IRobotAlina.Web.Services.TextExtractor
{
    public interface IDocumentTextExtractorFactory
    {
        public IDocumentTextExtractor GetInstance(string fileName);
    }
}
