using IRobotAlina.Web.BackgroundJob;
using NamedPipeWrapper;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.PrepareExcelFile
{
    public class PrepareExcelFile : IPrepareExcelFile
    {
        public Task Prepare(int mailId, string fileName, byte[] content)
        {
            string preparedFileName = Hangfire.BackgroundJob.Enqueue<OuterPrepareExcelFileService>(x => x.Execute(mailId, fileName, content));

            return Task.CompletedTask;
        }
    }
}
