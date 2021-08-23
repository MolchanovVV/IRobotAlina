using IRobotAlina.Data.Entities;
using System.Linq;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.Configuration
{
    public class DBZakupkiKonturCredentialsProvider : IZakupkiKonturCredentialsProvider
    {
        private readonly ApplicationDbContext dbContext;

        public DBZakupkiKonturCredentialsProvider(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ZakupkiKonturCredentials GetCredentials()
        {
            var items = dbContext.ConfigurationItems
                .Where(x => x.Type == EConfigurationItemType.ZakupkiEmail
                || x.Type == EConfigurationItemType.ZakupkiPassword).ToList();

            var creds = new ZakupkiKonturCredentials
            {
                Email = items.FirstOrDefault(x => x.Type == EConfigurationItemType.ZakupkiEmail).Value,
                Password = items.FirstOrDefault(x => x.Type == EConfigurationItemType.ZakupkiPassword).Value,
            };

            return creds;
        }
    }
}
