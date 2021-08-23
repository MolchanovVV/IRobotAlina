using System.Collections.Generic;

namespace IRobotAlina.Web.Services.Configuration
{
    public interface IMailFilteringConfigurationProvider
    {
        public IEnumerable<string> GetSenderEmailsToInclude();
    }
}
