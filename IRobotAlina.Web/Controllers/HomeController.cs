using System;
using System.Diagnostics;
using System.Linq;
using Hangfire;
using IRobotAlina.Web.BackgroundJob;
using IRobotAlina.Web.Models;
using IRobotAlina.Web.Services.Configuration;
using Microsoft.AspNetCore.Mvc;
using TenderDocumentsScraper.Data;
using TenderDocumentsScraper.Models;

namespace TenderDocumentsScraper.Controllers
{
    public class HomeController : Controller
    {        
        private readonly ValidateConfigurationSettings validateConfigurationSettings;
        private readonly ApplicationDbContext ctx;

        public HomeController(
            ValidateConfigurationSettings validateConfigurationSettings,
            ApplicationDbContext applicationDbContext)
        {         
            this.validateConfigurationSettings = validateConfigurationSettings;
            this.ctx = applicationDbContext;
        }

        public IActionResult Index(int CurrentPage = 1)
        {
            var api = JobStorage.Current.GetMonitoringApi();
            var russianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");

            var scheduledJobs = api.ScheduledJobs(0, 1000)
                .Where(x => x.Value.Job.Type == typeof(BackgroundMailService))
                .Select(x => new JobItem()
                {
                    Name = "Обработчик писем Zakupki.Kontur",
                    Status = $"Ожидает письмо, проверит почту в {TimeZoneInfo.ConvertTimeFromUtc(x.Value.EnqueueAt, russianTimeZone)}"
                });

            var runningJobs = api.ProcessingJobs(0, 1000)
                .Where(x => x.Value.Job.Type == typeof(BackgroundMailService))

                .Select(x => new JobItem()
                {
                    Name = "Обработчик писем Zakupki.Kontur",
                    Status = $"Обрабатывает входящие письма. Начал работу в {TimeZoneInfo.ConvertTimeFromUtc(x.Value.StartedAt.GetValueOrDefault(), russianTimeZone)}"
                });

            var vm = new HomeViewModel()
            {
                Jobs = scheduledJobs.Concat(runningJobs).ToList(),
                TotalTendersCount = ctx.Tenders.Count(),
                AreAllConfigurationsValid = validateConfigurationSettings.IsAllConfigurationPresent()
            };

            var pageSize = 10;
            var tenderMailsQuery = ctx.TenderMails;
            var tenderMailsCount = tenderMailsQuery.Count();

            var tenderMailPaginationModel = new BasePaginationModel<TenderMailViewModel>()
            {
                PageSize = 10,
                Count = tenderMailsCount,
                CurrentPage = CurrentPage,
                Data = tenderMailsQuery
                    .OrderByDescending(x => x.SentDateTime)
                    .Skip((CurrentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new TenderMailViewModel()
                    {
                        Id = x.Id,
                        IsProcessed = x.IsProcessed,
                        Name = x.Name,
                        TenderCount = x.Tenders.Count(),
                        SentDateTime = x.SentDateTime
                    }).ToList()
            };

            vm.TenderMails = tenderMailPaginationModel;

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
