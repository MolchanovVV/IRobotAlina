using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using IRobotAlina.Web.BackgroundJob;
using System.Threading;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.PrepareExcelFile
{
    public class PrepareExcelFile : IPrepareExcelFile
    {
        public Task Prepare(int mailId, string fileName, byte[] content)
        {
            string jobId = Hangfire.BackgroundJob.Enqueue<OuterPrepareExcelFileService>(x => x.Execute(mailId, fileName, content));

            Task checkJobState = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    IMonitoringApi monitoringApi = JobStorage.Current.GetMonitoringApi();
                    JobDetailsDto jobDetails = monitoringApi.JobDetails(jobId);

                    string currentState = jobDetails.History[0].StateName;
                    if (currentState != "Enqueued" && currentState != "Processing")
                    {
                        break;
                    }

                    Thread.Sleep(100); // adjust to a coarse enough value for your scenario
                }

                Thread.Sleep(1000);
            });
            
            return checkJobState;            
        }
    }
}
