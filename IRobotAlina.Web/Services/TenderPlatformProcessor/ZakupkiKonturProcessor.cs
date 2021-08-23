using IRobotAlina.Data.Entities;
using IRobotAlina.Web.Utils;
using IRobotAlina.Web.Services.Builder;
using IRobotAlina.Web.Services.Download;
using IRobotAlina.Web.Services.Files;
using IRobotAlina.Web.Services.Scraper;
using IRobotAlina.Web.Services.Storage;
using IRobotAlina.Web.Services.TextExtractor;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using IRobotAlina.Web.Services.PrepareExcelFile;

namespace IRobotAlina.Web.Services.TenderPlatformProcessor
{
    public class ZakupkiKonturProcessor : ITenderPlatformProcessor
    {
        private readonly IZakupkiKonturScraper scraper;
        private readonly ISaveTenderFileAttachment saveTenderFile;
        private readonly IZakupkiKonturTenderMailProvider linkProvider;
        private readonly IZakupkiKonturTenderBuilder tenderBuilder;
        private readonly DownloadFileClient downloadFileClient;
        private readonly FileService fileService;
        private readonly IDocumentTextExtractorFactory documentTextExtractorFactory;
        private readonly IPrepareExcelFile prepareExcelFile;
        
        public ZakupkiKonturProcessor(
            IZakupkiKonturScraper scraper,
            ISaveTenderFileAttachment saveTenderFile,
            IZakupkiKonturTenderMailProvider linkProvider,
            IZakupkiKonturTenderBuilder tenderBuilder,
            DownloadFileClient downloadFileClient,
            FileService fileService,
            IDocumentTextExtractorFactory documentTextExtractorFactory,
            IPrepareExcelFile prepareExcelFile
        )
        {
            this.scraper = scraper;
            this.saveTenderFile = saveTenderFile;
            this.linkProvider = linkProvider;
            this.tenderBuilder = tenderBuilder;
            this.downloadFileClient = downloadFileClient;
            this.fileService = fileService;
            this.documentTextExtractorFactory = documentTextExtractorFactory;
            this.prepareExcelFile = prepareExcelFile;
        }

        public async Task Execute()
        {
            var tenderMails = await linkProvider.GetTenderMails();

            foreach (var tenderMail in tenderMails)
            {
                string linkFileTenderAdditionalPart = tenderMail.Links.Where(p => p.Type == Mails.EMailLinkType.Download).Select(s => s.Link)?.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(linkFileTenderAdditionalPart))
                {
                    var resDownload = await scraper.DownloadFileTendersAdditionalPart(linkFileTenderAdditionalPart);

                    if (resDownload)
                    {
                        string downloadFolderPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads");

                        string filePath = Directory.GetFiles(downloadFolderPath).Where(f => Path.GetExtension(f) == ".xlsx" || Path.GetFileName(f).Contains("Контур")).OrderBy(f => File.GetLastWriteTime(f))?.LastOrDefault();
                        string fileName = Path.GetFileName(filePath);

                        string fileMainStorageConture = Path.Combine(Environment.Current​Directory, "Контур", "Оригиналы");

                        if (!Directory.Exists(fileMainStorageConture))
                            Directory.CreateDirectory(fileMainStorageConture);

                        File.Move(filePath, Path.Combine(fileMainStorageConture, fileName), true);

                        string fileWorkStorageConture = Path.Combine(Environment.Current​Directory, "Контур", "Рабочие");

                        if (!Directory.Exists(fileWorkStorageConture))
                            Directory.CreateDirectory(fileWorkStorageConture);

                        string workFilePath = Path.Combine(fileWorkStorageConture, fileName);
                        File.Copy(Path.Combine(fileMainStorageConture, fileName), workFilePath, true);

                        try
                        {                            
                            await prepareExcelFile.Prepare(tenderMail.Id.Value, fileName, File.ReadAllBytes(workFilePath));
                        }
                        catch(Exception ex)
                        {
                            string ee = ex.Message;
                        }
                    }


                    //var fileTenderAdditionalPart = await downloadFileClient.DownloadFile(linkFileTenderAdditionalPart, scraper.GetCookies());

                    //if (fileTenderAdditionalPart != null)
                    //{
                    //    File.WriteAllBytes(@"C:\Test\fileTenderAdditionalPart.xls", fileTenderAdditionalPart);
                    //}
                }
                                
                int linkIdx = 1;

                foreach (var tenderLink in tenderMail.Links.Where(p => p.Type == Mails.EMailLinkType.Typical).Select(s => s.Link))
                {
                    var tenderInfo = await scraper.GetTenderInfo(tenderLink);

                    if (tenderInfo == null)
                        continue;

                    var tmpTender = new Tender()
                    {
                        Url = tenderLink,
                        Name = tenderInfo.Name,
                        TenderMailId = tenderMail.Id.Value,
                        Description = tenderInfo.Description,
                        Order = linkIdx++,
                    };

                    var tender = tenderBuilder.GetOrCreate(tmpTender);

                    var fileAttachmentLinks = tenderInfo.Documents;

                    var newFileAttachmentLinks = fileAttachmentLinks
                        .Where(x => !tender.rootFiles.Any(y => y.Name == x.FileName))
                        .ToList(); // save only files that do not exist yet

                    foreach (var attachmentLink in newFileAttachmentLinks)
                    {
                        var fileContent = await downloadFileClient.DownloadFile(attachmentLink.Url, scraper.GetCookies());

                        if (fileContent != null)
                        {
                            foreach (var file in fileService.ExtractFiles(attachmentLink.FileName, fileContent))
                            {
                                var attachment = new TenderFileAttachment()
                                {
                                    Content = file.FileContent,
                                    TenderId = tender.tender.Id,
                                    FileName = file.FileName,
                                    FullPath = file.FilePath,
                                    ArchiveName = file.ArchiveName,
                                    IsArchive = FileUtils.IsFileArchive(file.FileName)
                                };

                                await saveTenderFile.Save(attachment);

                                var documentTextExtractor = documentTextExtractorFactory.GetInstance(file.FileName);
                                if (documentTextExtractor != null)
                                {
                                    attachment.Status = ETenderFileAttachmentStatus.InProgress;
                                    await documentTextExtractor.Extract(attachment);                                    
                                }

                                if (attachment.Status == null)
                                {
                                    attachment.Status = ETenderFileAttachmentStatus.InProgress; // если мы наткнулись на тип файла, который не имеет обработчика
                                    await saveTenderFile.Update(attachment);                                    
                                }

                                // await saveTenderFile.Update(attachment);

                                // await saveTenderFile.Save(attachment);
                            }
                        }
                    }                    
                }

                linkProvider.MarkAsCompleted(tenderMail);
            }

            //linkProvider.MarkAsCompleted(tenderMails);
        }
    }
}
