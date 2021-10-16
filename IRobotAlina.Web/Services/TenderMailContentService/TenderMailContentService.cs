using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace IRobotAlina.Web.Services.TenderMailContentService
{
    public class TenderMailContentService : ITenderMailContentService
    {
        private const int CntTryCreateTenderMailContent = 10;
        private readonly ApplicationDbContext applicationDbContext;

        public TenderMailContentService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void CreateTenderMailContent(List<int> mailIds)
        {
            Dictionary<int, bool> mails = new Dictionary<int, bool>();

            foreach (var mailId in mailIds)
            {                
                mails.Add(mailId, false);
            }

            if (mails.Count == 0)
                return;

            int cnt = 0; int checkResult;
            try
            {
                while (mails.ContainsValue(false) && cnt <= CntTryCreateTenderMailContent)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(60));

                    foreach (var id in mailIds)
                    {
                        if (!mails.ContainsKey(id) || mails[id])
                            continue;

                        SqlParameter mailId = new SqlParameter("@mailId", id);
                        SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int);
                        result.Direction = System.Data.ParameterDirection.Output;

                        applicationDbContext.Database.ExecuteSqlRaw("p_CheckTenderFilesProcessingComplite @mailId, @result output", new[] { mailId, result });
                        checkResult = Convert.ToInt32(result.Value);

                        if (checkResult == 0 || cnt == CntTryCreateTenderMailContent)
                        {
                            applicationDbContext.Database.ExecuteSqlRaw("p_CreateTenderMailContent @mailId", mailId);
                            mails[id] = true;
                        }
                    }

                    cnt++;
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
