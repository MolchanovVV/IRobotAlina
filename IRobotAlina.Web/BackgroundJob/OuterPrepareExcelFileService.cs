using Hangfire;
using IRobotAlina.Web.Services.PrepareExcelFile;
using Microsoft.EntityFrameworkCore;
using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;
using IRobotAlina.Data.Entities;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace IRobotAlina.Web.BackgroundJob
{
    public class OuterPrepareExcelFileService : IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private readonly NamedPipeClient_PrepareExcelFileService service;

        public OuterPrepareExcelFileService(
            ApplicationDbContext dbContext,
            NamedPipeClient_PrepareExcelFileService service)
        {
            this.dbContext = dbContext;
            this.service = service;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Queue("beta")]
        public async Task Execute(int mailId, string fileName, byte[] content)        
        {
            var tenderMailFile = dbContext.TenderMailFiles.FirstOrDefault(x => x.TenderMailId == mailId && x.Type == Data.Entities.Enums.ETenderMailFileType.TenderAdditionalPart );
            bool isNew = false;

            if (tenderMailFile == null)            
            {
                isNew = true;
                tenderMailFile = new TenderMailFile() { TenderMailId = mailId, Type = Data.Entities.Enums.ETenderMailFileType.TenderAdditionalPart };
            }

            tenderMailFile.Name = fileName;
            tenderMailFile.Content = content;

            try
            {                
                Exl_DataMessage dataMessage = await service.SendRequestToPrepareExcelFile(mailId, fileName, content);                
                tenderMailFile.ParsedData = dataMessage.serviceResult;
                tenderMailFile.DateParsing = DateTime.Now;
                tenderMailFile.Status = Data.Entities.Enums.ETenderMailFileStatus.Successful;
            }
            catch (Exception ex)
            {
                tenderMailFile.ParsedData = ex.Message;
                tenderMailFile.Status = Data.Entities.Enums.ETenderMailFileStatus.Error;
            }
            finally
            {
                if (isNew)
                {
                    dbContext.Add(tenderMailFile);
                }
                else
                {
                    dbContext.Update(tenderMailFile);
                }

                //dbContext.Entry(tenderMailFile).Property(x => x.Content).IsModified = false;
                dbContext.SaveChanges();
                dbContext.DetachAllEntities();
            }            
        }        
    }
}
