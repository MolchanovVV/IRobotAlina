using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.HeartbeatProvider
{
    public class HeartbeatProvider : IHeartbeatProvider
    {
        private readonly ApplicationDbContext applicationDbContext;

        public HeartbeatProvider(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task SetKnockKnock()
        {
            await applicationDbContext.Database.ExecuteSqlRawAsync("p_SetJustKnockKnock");            
        }
    }
}
