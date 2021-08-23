using IRobotAlina.Data.Entities;
using System.Linq;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.Configuration
{
    public class DBEWSConfigurationProvider : IEWSConfigurationProvider
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DBEWSConfigurationProvider(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public EWSConfiguration GetEWSConfiguration()
        {
            var items = applicationDbContext.ConfigurationItems
                .Where(x => x.Type == EConfigurationItemType.EWSIP
                || x.Type == EConfigurationItemType.EWSLogin
                || x.Type == EConfigurationItemType.EWSPassword).ToList();

            var config = new EWSConfiguration()
            {
                IP = items.FirstOrDefault(x => x.Type == EConfigurationItemType.EWSIP).Value,
                UserIdentity = items.FirstOrDefault(x => x.Type == EConfigurationItemType.EWSLogin).Value,
                Password = items.FirstOrDefault(x => x.Type == EConfigurationItemType.EWSPassword).Value,
            };

            return config;
        }
    }
}
