using IRobotAlina.Data.Entities;
using IRobotAlina.Web.Models;
using IRobotAlina.Web.Services.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ValidateConfigurationSettings validateConfigurationSettings;

        public ConfigurationController(ApplicationDbContext dbContext, 
            ValidateConfigurationSettings validateConfigurationSettings)
        {
            this.dbContext = dbContext;
            this.validateConfigurationSettings = validateConfigurationSettings;
        }

        public IActionResult Index()
        {
            var configurationItems = dbContext.ConfigurationItems.ToList();
            string getValue(EConfigurationItemType type) => configurationItems.FirstOrDefault(x => x.Type == type).Value;

            var vm = new ConfigurationViewModel()
            {
                EWSIP = getValue(EConfigurationItemType.EWSIP),
                EWSLogin = getValue(EConfigurationItemType.EWSLogin),
                EWSPassword = getValue(EConfigurationItemType.EWSPassword),
                MailHostNames = getValue(EConfigurationItemType.MailHostNames),
                ZakupkiEmail = getValue(EConfigurationItemType.ZakupkiEmail),
                ZakupkiPassword = getValue(EConfigurationItemType.ZakupkiPassword)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ConfigurationViewModel vm)
        {
            var configurationItems = dbContext.ConfigurationItems.ToList();

            void setValue(EConfigurationItemType type, string value)
            {
                var item = configurationItems.FirstOrDefault(x => x.Type == type);

                item.Value = value;

                dbContext.Update(item);
            }

            setValue(EConfigurationItemType.EWSIP, vm.EWSIP);
            setValue(EConfigurationItemType.EWSLogin, vm.EWSLogin);
            setValue(EConfigurationItemType.EWSPassword, vm.EWSPassword);

            setValue(EConfigurationItemType.MailHostNames, vm.MailHostNames);

            setValue(EConfigurationItemType.ZakupkiEmail, vm.ZakupkiEmail);
            setValue(EConfigurationItemType.ZakupkiPassword, vm.ZakupkiPassword);

            dbContext.SaveChanges();

            return View();
        }

        public IActionResult CheckSettings()
        {
            var (isSuccess, errorMessage) = validateConfigurationSettings.CheckEWSSettings();

            var vm = new CheckConfigurationViewModel()
            {
                IsEWSWorking = isSuccess,
                EWSErrorMessage = errorMessage,
                IsZakupkiAuthWorking = validateConfigurationSettings.CheckZakupkiSettings()
            };

            return View(vm);
        }
    }
}
