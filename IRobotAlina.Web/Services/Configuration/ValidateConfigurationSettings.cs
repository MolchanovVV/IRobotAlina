using IRobotAlina.Web.Services.Mails;
using IRobotAlina.Web.Services.Scraper;
using System;
using System.Linq;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.Configuration
{
    public class ValidateConfigurationSettings
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IZakupkiKonturScraper scraper;
        private readonly EWSMailProvider mailProvider;

        public ValidateConfigurationSettings(ApplicationDbContext applicationDbContext,
             IZakupkiKonturScraper scraper,
             EWSMailProvider mailProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.scraper = scraper;
            this.mailProvider = mailProvider;
        }

        public bool IsAllConfigurationPresent()
        {
            var items = applicationDbContext.ConfigurationItems
                .Where(x => x.Type != Data.Entities.EConfigurationItemType.MailHostNames)
                .ToList();

            return items.All(x => string.IsNullOrWhiteSpace(x.Value) == false);
        }

        public bool CheckZakupkiSettings()
        {
            try
            {
                return scraper.GetCookies().Any();
            }
            catch
            {
                return false;
            }
        }

        public (bool isSuccess, string errorMessage) CheckEWSSettings()
        {
            try
            {
                mailProvider.Initialize();

                return (true, null);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
