using IRobotAlina.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.Configuration
{
    public class DBMailFilteringConfigurationProvider : IMailFilteringConfigurationProvider
    {
        private readonly ApplicationDbContext dbContext;

        public DBMailFilteringConfigurationProvider(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<string> GetSenderEmailsToInclude()
        {
            var emails = dbContext.ConfigurationItems
                .Where(x => x.Type == EConfigurationItemType.MailHostNames)
                .Select(x => x.Value)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(emails))
            {
                return new List<string>();
            }

            return emails.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
