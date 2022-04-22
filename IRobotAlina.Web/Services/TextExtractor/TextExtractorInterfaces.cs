using IRobotAlina.Data.Entities;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.TextExtractor
{
    public interface IDocumentTextExtractor
    {
        public Task Extract(IAttachment attachment);
    }

    public interface ITxtDocumentTextExtractor : IDocumentTextExtractor
    { }

    public interface IPdfDocumentTextExtractor : IDocumentTextExtractor
    { }

    public interface IImageDocumentTextExtractor : IDocumentTextExtractor
    { }

    public interface IDocDocumentTextExtractor : IDocumentTextExtractor
    { }

    public interface IDocXDocumentTextExtractor : IDocumentTextExtractor
    { }

    public interface IXlsDocumentTextExtractor : IDocumentTextExtractor
    { }

    public interface IXlsxDocumentTextExtractor : IDocumentTextExtractor
    { }
}
