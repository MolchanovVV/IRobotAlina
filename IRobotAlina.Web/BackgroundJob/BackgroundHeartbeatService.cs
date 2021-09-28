using Hangfire;
using IRobotAlina.Web.Services.HeartbeatProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IRobotAlina.Web.BackgroundJob
{
    [AutomaticRetry(Attempts = 0)]
    public class BackgroundHeartbeatService
    {
        public const string jobId = "set-heartbeat-knock";
        private readonly IServiceProvider serviceProvider;
        
        public BackgroundHeartbeatService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;            
        }
       
        [Queue("alpha")]
        public async Task ExecuteAsync()
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<BackgroundHeartbeatService>>();
            var backgroundJobClient = scope.ServiceProvider.GetService<IBackgroundJobClient>();

            try
            {
                var processingJobs = JobStorage.Current.GetMonitoringApi().ProcessingJobs(0, 10);
                if (processingJobs.Count(x => x.Key == jobId) > 1)
                {
                    return;
                }

                var processor = scope.ServiceProvider.GetService<IHeartbeatProvider>();

                await processor.SetKnockKnock();

                backgroundJobClient.Schedule(() => new BackgroundHeartbeatService(serviceProvider).ExecuteAsync(), TimeSpan.FromMinutes(5));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{jobId} finished execution with error, queueing next instance");

                backgroundJobClient.Schedule(() => new BackgroundHeartbeatService(serviceProvider).ExecuteAsync(), TimeSpan.FromMinutes(5));
                throw;
            }            
        }
    }
}
