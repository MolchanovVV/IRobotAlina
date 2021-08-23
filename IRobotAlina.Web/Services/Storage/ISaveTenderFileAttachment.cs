using IRobotAlina.Data.Entities;
using System.IO;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.Storage
{
    public interface ISaveTenderFileAttachment
    {
        public Task Save(TenderFileAttachment attachment);
        public Task Update(TenderFileAttachment attachment);
    }
}
