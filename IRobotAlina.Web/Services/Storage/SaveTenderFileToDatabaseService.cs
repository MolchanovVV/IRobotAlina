using IRobotAlina.Data.Entities;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.Storage
{
    public class SaveTenderFileToDatabaseService : ISaveTenderFileAttachment
    {
        private readonly ApplicationDbContext dbContext;

        public SaveTenderFileToDatabaseService(ApplicationDbContext applicationDbContext)
        {
            this.dbContext = applicationDbContext;
        }

        public async Task Save(TenderFileAttachment attachment)
        {
            // 23.03.2021 -Молчанов -Контент архивов в базе не нужен
            if (attachment.IsArchive)
                attachment.Content = null;

            dbContext.TenderFileAttachments.Add(attachment);
            await dbContext.SaveChangesAsync();

            dbContext.DetachAllEntities();
        }

        public async Task Update(TenderFileAttachment attachment)
        {
            dbContext.TenderFileAttachments.Update(attachment);
            dbContext.Entry(attachment).Property(x => x.Content).IsModified = false;
            await dbContext.SaveChangesAsync();

            dbContext.DetachAllEntities();
        }
    }
}
