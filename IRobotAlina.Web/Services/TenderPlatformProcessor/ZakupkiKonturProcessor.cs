using IRobotAlina.Data.Entities;
using IRobotAlina.Web.Services.Builder;
using IRobotAlina.Web.Services.Download;
using IRobotAlina.Web.Services.Files;
using IRobotAlina.Web.Services.PrepareExcelFile;
using IRobotAlina.Web.Services.Scraper;
using IRobotAlina.Web.Services.Storage;
using IRobotAlina.Web.Services.TenderMailContentService;
using IRobotAlina.Web.Services.TenderMailFiles;
using IRobotAlina.Web.Services.TextExtractor;
using IRobotAlina.Web.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly ITenderMailFileProvider tenderMailFileProvider;
        private readonly IParseTenderAdditionalPartExcelData parseTenderAdditionalPartExcelData;
        private readonly ITenderMailContentService tenderMailContentService;

        public ZakupkiKonturProcessor(
            IZakupkiKonturScraper scraper,
            ISaveTenderFileAttachment saveTenderFile,
            IZakupkiKonturTenderMailProvider linkProvider,
            IZakupkiKonturTenderBuilder tenderBuilder,
            DownloadFileClient downloadFileClient,
            FileService fileService,
            IDocumentTextExtractorFactory documentTextExtractorFactory,
            IPrepareExcelFile prepareExcelFile,
            ITenderMailFileProvider tenderMailFileProvider,
            IParseTenderAdditionalPartExcelData parseTenderAdditionalPartExcelData,
            ITenderMailContentService tenderMailContentService
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
            this.tenderMailFileProvider = tenderMailFileProvider;
            this.parseTenderAdditionalPartExcelData = parseTenderAdditionalPartExcelData;
            this.tenderMailContentService = tenderMailContentService;
        }

        public async Task Execute()
        {
            var tenderMails = await linkProvider.GetTenderMails();

            foreach (var tenderMail in tenderMails)
            {
                string linkFileTenderAdditionalPart = tenderMail.Links.Where(p => p.Type == Mails.EMailLinkType.Download).Select(s => s.Link)?.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(linkFileTenderAdditionalPart))
                {
                    //bool resDownload = await scraper.DownloadFileTendersAdditionalPart(linkFileTenderAdditionalPart);
                    scraper.DownloadFileTendersAdditionalPart(linkFileTenderAdditionalPart);

                    string downloadFolderPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads");

                    string sourceFileName = Directory.GetFiles(downloadFolderPath)
                        .Where(p =>
                            (Path.GetExtension(p) == ".xls" || Path.GetExtension(p) == ".xlsx") &&
                            Path.GetFileName(p).Contains("Контур")
                        )
                        .OrderBy(p => File.GetLastWriteTime(p))?.LastOrDefault();

                    if (!string.IsNullOrEmpty(sourceFileName))
                    {
                        DateTime lastWriteTime = Directory.GetLastWriteTime(sourceFileName);
                        string destFileName = $"{Path.GetFileNameWithoutExtension(sourceFileName)} ({lastWriteTime.ToString("dd.MM.yyyy HH-mm-ss")}){Path.GetExtension(sourceFileName)}";

                        string fileMainStorageConture = Path.Combine(Environment.Current​Directory, "Контур");

                        if (!Directory.Exists(fileMainStorageConture))
                            Directory.CreateDirectory(fileMainStorageConture);

                        File.Move(sourceFileName, Path.Combine(fileMainStorageConture, destFileName), true);

                        await prepareExcelFile.Prepare(tenderMail.Id.Value, destFileName, File.ReadAllBytes(Path.Combine(fileMainStorageConture, destFileName)));
                    }
                }

                string parsedData = tenderMailFileProvider.GetTenderMailFiles(tenderMail.Id.Value)
                    ?.Where(p => p.Type == Data.Entities.Enums.ETenderMailFileType.TenderAdditionalPart && p.Status == Data.Entities.Enums.ETenderMailFileStatus.Successful)
                    ?.Select(s => s.ParsedData)
                    ?.FirstOrDefault();

                List<Tender> tenderAdditionalParts = parseTenderAdditionalPartExcelData.GetTenderAdditionalPart(parsedData);

                int linkIdx = 1;

                foreach (var tenderLink in tenderMail.Links.Where(p => p.Type == Mails.EMailLinkType.Typical).Select(s => s.Link))
                {
                    var tenderInfo = await scraper.GetTenderInfo(tenderLink);

                    if (tenderInfo == null)
                        continue;

                    var tmpTender = new Tender() 
                    {
                        TenderMailId = tenderMail.Id.Value,
                        Number = tenderInfo.Number,
                        Name = tenderInfo.Name,
                        Url = tenderLink,
                        Description = tenderInfo.Description,
                        Order = linkIdx++
                    };

                    var tender = tenderBuilder.GetOrCreate(tmpTender, tenderAdditionalParts.Where(p => p.Number == tmpTender.Number)?.FirstOrDefault());

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
                                var attachment = new TenderFileAttachment(
                                    tenderId: tender.tender.Id,
                                    fileName: file.FileName,
                                    fullPath: file.FilePath,
                                    archiveName: file.ArchiveName,
                                    content: file.FileContent,
                                    isArchive: FileUtils.IsFileArchive(file.FileName)
                                );

                                await saveTenderFile.Save(attachment);

                                var documentTextExtractor = documentTextExtractorFactory.GetInstance(file.FileName);
                                if (documentTextExtractor != null)
                                {
                                    attachment.Status = ETenderFileAttachmentStatus.InProgress;
                                    await documentTextExtractor.Extract(attachment);
                                }

                                if (attachment.Status == ETenderFileAttachmentStatus.Unknown)
                                {
                                    attachment.Status = ETenderFileAttachmentStatus.NotProcessed; // если мы наткнулись на тип файла, который не имеет обработчика
                                    await saveTenderFile.Update(attachment);
                                }
                            }
                        }
                    }
                }

                linkProvider.MarkAsCompleted(tenderMail);
            }

            List<int> mailIds = tenderMails.Where(p => p.Id.HasValue).Select(s => s.Id.Value).ToList();
            tenderMailContentService.CreateTenderMailContent(mailIds);
        }
    }
}