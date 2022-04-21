using IRobotAlina.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.TenderMailFiles
{
    public class TenderMailFileProvider : ITenderMailFileProvider
    {
        private readonly ApplicationDbContext dbContext;

        public TenderMailFileProvider(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<TenderMailFile> GetTenderMailFiles(int mailId)
        {            
            return dbContext.TenderMailFiles.Where(s => s.TenderMailId == mailId).ToList();            
        }
    }
}
