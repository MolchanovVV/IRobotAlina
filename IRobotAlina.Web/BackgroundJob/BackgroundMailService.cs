using Hangfire;
using IRobotAlina.Web.Services.TenderPlatformProcessor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IRobotAlina.Web.BackgroundJob
{
    [AutomaticRetry(Attempts = 0)]
    public class BackgroundMailService
    {
        public const string jobId = "mail-tender-processor";
        private readonly IServiceProvider serviceProvider;

        public BackgroundMailService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [Queue("alpha")]
        public async Task ExecuteAsync()
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<BackgroundMailService>>();
            var backgroundJobClient = scope.ServiceProvider.GetService<IBackgroundJobClient>();

            try
            {
                var processingJobs = JobStorage.Current.GetMonitoringApi().ProcessingJobs(0, 10);                
                if (processingJobs.Count(x => x.Key == jobId) > 1)
                {                    
                    return;
                }

                var processor = scope.ServiceProvider.GetService<ITenderPlatformProcessor>();

                await processor.Execute();
                
                backgroundJobClient.Schedule(() => new BackgroundMailService(serviceProvider).ExecuteAsync(), TimeSpan.FromMinutes(5));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{jobId} finished execution with error, queueing next instance");
                
                backgroundJobClient.Schedule(() => new BackgroundMailService(serviceProvider).ExecuteAsync(), TimeSpan.FromMinutes(5));
                throw;
            }

        }
    }
}
