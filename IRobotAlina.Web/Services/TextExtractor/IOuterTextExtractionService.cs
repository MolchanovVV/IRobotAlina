using System;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.TextExtractor
{
    public interface IOuterTextExtractionService
    {
        public Task TextExtract(Guid id);
    }
}
