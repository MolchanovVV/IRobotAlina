using Hangfire;
using IRobotAlina.Web.Services.PrepareExcelFile;
using Microsoft.EntityFrameworkCore;
using NamedPipeWrapper;
using System;
using System.IO;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

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
            try
            {
                Exl_DataMessage dataMessage = await service.SendRequestToPrepareExcelFile(mailId, fileName, content);

                if (dataMessage.type == DataMessageSettings.MessageType.Error)
                    throw new Exception(dataMessage.errMsg);

                //string preparedFileName = Path.Combine(Environment.Current​Directory, "Контур", "Рабочие", string.Concat(Path.GetFileNameWithoutExtension(fileName), "_Prepared", Path.GetExtension(fileName)));

                //string preparedFileName = Path.Combine(@"C:\ForLinkedServer", string.Concat(Path.GetFileNameWithoutExtension(fileName), "_Prepared", Path.GetExtension(fileName)));
                //File.WriteAllBytes(preparedFileName, dataMessage.content);

                //dbContext.Database.ExecuteSqlRaw($"exec [dbo].[p_FillTmpTenderAddPart] @mailId = {mailId}, @fileName = '{preparedFileName}'");

                //dbContext.Database.ExecuteSqlRaw($"exec [dbo].[p_FillTmpTenderAddPart] @mailId = {mailId}, @fileName = 'C:\\ForLinkedServer\\Контур.Закупки_13.06.2021_Prepared.xlsx'");
                

                //dbContext.Database.ExecuteSqlRaw($"exec [dbo].[p_CheckSPExec] @mailId = {mailId}, @value = 1");
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
