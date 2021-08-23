using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.PrepareExcelFile
{
    public interface IPrepareExcelFile
    {
        public Task Prepare(int mailId, string fileName, byte[] content);
    }
}
