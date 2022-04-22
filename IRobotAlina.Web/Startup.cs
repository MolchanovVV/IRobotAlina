using Hangfire;
using IRobotAlina.Web.BackgroundJob;
using IRobotAlina.Web.Services;
using IRobotAlina.Web.Services.Builder;
using IRobotAlina.Web.Services.Configuration;
using IRobotAlina.Web.Services.Download;
using IRobotAlina.Web.Services.Files;
using IRobotAlina.Web.Services.HeartbeatProvider;
using IRobotAlina.Web.Services.Mails;
using IRobotAlina.Web.Services.PrepareExcelFile;
using IRobotAlina.Web.Services.Scraper;
using IRobotAlina.Web.Services.Storage;
using IRobotAlina.Web.Services.TenderLinkProvider;
using IRobotAlina.Web.Services.TenderMailContentService;
using IRobotAlina.Web.Services.TenderMailFiles;
using IRobotAlina.Web.Services.TenderPlatformProcessor;
using IRobotAlina.Web.Services.TextExtractor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text;
using TenderDocumentsScraper.Data;

namespace TenderDocumentsScraper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                    , opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds))
            );

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddHangfireServer(options =>
            {
                options.ServerName = $"{Environment.MachineName}:main";
                options.WorkerCount = Environment.ProcessorCount * 5;
                options.Queues = new[] { "main" };
            });

            services.AddHangfireServer(options =>
            {
                options.ServerName = $"{Environment.MachineName}:pdf";
                options.WorkerCount = 1;
                options.Queues = new[] { "pdf" };
            });

            services.AddHangfireServer(options =>
            {
                options.ServerName = $"{Environment.MachineName}:jpg";
                options.WorkerCount = 1;
                options.Queues = new[] { "jpg" };
            });

            services.AddHttpClient<SeleniumZakupkiKonturScraper>();

            services.AddScoped<ITenderPlatformProcessor, ZakupkiKonturProcessor>();
            services.AddScoped<IDocumentTextExtractorFactory, DocumentTextExtractorFactory>();
            services.AddScoped<IPdfDocumentTextExtractor, OuterTextExtractor>();
            services.AddScoped<IImageDocumentTextExtractor, OuterTextExtractor>();
            services.AddScoped<IDocDocumentTextExtractor, OuterTextExtractor>();
            services.AddScoped<IDocXDocumentTextExtractor, OuterTextExtractor>();
            services.AddScoped<IXlsDocumentTextExtractor, OuterTextExtractor>();
            services.AddScoped<IXlsxDocumentTextExtractor, OuterTextExtractor>();
            services.AddScoped<ITxtDocumentTextExtractor, InnerTextExtractor>();
            services.AddScoped<NamedPipeClient_TextExtractionService>();
            services.AddScoped<ITemporaryStoragePathProvider, TemporaryStoragePathProvider>();
            services.AddScoped<IZakupkiKonturScraper, SeleniumZakupkiKonturScraper>();
            services.AddScoped<IZakupkiKonturTenderBuilder, ZakupkiKonturTenderBuilder>();
            services.AddScoped<ISaveTenderFileAttachment, SaveTenderFileToDatabaseService>();
            services.AddScoped<DownloadFileClient>();
            services.AddScoped<FileService>();
            services.AddScoped<IZakupkiKonturMailParser, ZakupkiKonturMailParser>();
            //services.AddScoped<IZakupkiKonturTenderLinkProvider, ZakupkiKonturDummyEmailTenderLinkProvider>();
            services.AddScoped<IZakupkiKonturTenderMailProvider, ZakupkiKonturEWSEmailTenderMailProvider>();
            services.AddScoped<EWSMailProvider>();
            services.AddScoped<IMailFilteringConfigurationProvider, DBMailFilteringConfigurationProvider>();
            services.AddScoped<IEWSConfigurationProvider, DBEWSConfigurationProvider>();
            services.AddScoped<IZakupkiKonturCredentialsProvider, DBZakupkiKonturCredentialsProvider>();
            services.AddScoped<ValidateConfigurationSettings>();
            services.AddScoped<IPrepareExcelFile, PrepareExcelFile>();
            services.AddScoped<NamedPipeClient_PrepareExcelFileService>();
            services.AddScoped<OuterTextExtractionService>();
            services.AddScoped<ITenderMailFileProvider, TenderMailFileProvider>();
            services.AddScoped<IParseTenderAdditionalPartExcelData, ParseTenderAdditionalPartExcelData>();
            services.AddScoped<IHeartbeatProvider, HeartbeatProvider>();
            services.AddScoped<ITenderMailContentService, TenderMailContentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, IBackgroundJobClient backgroundJobClient)
        {
            RunDatabaseMigrations(app);

            app.UseHangfireDashboard();

            ScheduleBackgroundJobs(backgroundJobClient, serviceProvider);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void ScheduleBackgroundJobs(IBackgroundJobClient backgroundJobClient, IServiceProvider serviceProvider)
        {
            var api = JobStorage.Current.GetMonitoringApi();
            var scheduledJobs = api.ScheduledJobs(0, 10);
            var processingJobs = api.ProcessingJobs(0, 10);

            if (scheduledJobs.Any(x => x.Value.Job.Type == typeof(BackgroundMailService)) == false
                && processingJobs.Any(x => x.Value.Job.Type == typeof(BackgroundMailService)) == false)
            {
                backgroundJobClient.Schedule(() => new BackgroundMailService(serviceProvider).ExecuteAsync(), TimeSpan.FromSeconds(10));
            }

            if (scheduledJobs.Any(x => x.Value.Job.Type == typeof(BackgroundHeartbeatService)) == false
                && processingJobs.Any(x => x.Value.Job.Type == typeof(BackgroundHeartbeatService)) == false)
            {
                backgroundJobClient.Schedule(() => new BackgroundHeartbeatService(serviceProvider).ExecuteAsync(), TimeSpan.FromSeconds(10));
            }
        }

        private static void RunDatabaseMigrations(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
