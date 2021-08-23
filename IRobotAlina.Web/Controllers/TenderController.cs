using System.Linq;
using IRobotAlina.Web.Models;
using Microsoft.AspNetCore.Mvc;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Controllers
{
    public class TenderController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public TenderController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index(int currentPage = 1, int? mailId = null)
        {
            var pageSize = 12;

            var query = dbContext.Tenders.AsQueryable();

            if (mailId.HasValue)
            {
                query = query.Where(x => x.TenderMailId == mailId);
            }

            var items = query
                .OrderBy(x => x.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TenderViewModel()
                {
                    Id = x.Id,
                    Order = x.Order,
                    FileCount = x.FileAttachments.Count(),
                    Name = x.Name,                    
                    Url = x.Url,
                    MailId = x.TenderMailId,
                    MailName = x.TenderMail.Name,
                }).ToList();

            var count = query.Count();

            int tenderFileAttachmentsCount = 0;
            if (mailId.HasValue)
            {
                tenderFileAttachmentsCount = dbContext.TenderFileAttachments.Where(x => x.Tender.TenderMailId == mailId).Count();
            }
            else
            {
                tenderFileAttachmentsCount = dbContext.TenderFileAttachments.Count();
            }

            var vm = new TenderPaginationModel()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Data = items,
                Count = count,                
                TotalFileCount = tenderFileAttachmentsCount
            };

            if (mailId.HasValue)
            {
                var tenderMail = dbContext.TenderMails.FirstOrDefault(x => x.Id == mailId);

                if (tenderMail != null)
                {
                    vm.MailName = tenderMail.Name;
                    vm.MailId = tenderMail.Id;
                }
            }

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var tender = dbContext.Tenders.Where(x => x.Id == id)
                .Select(x => new TenderDetailsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,                    
                    Url = x.Url,
                    ExternalId = x.ExternalId,
                    Attachments = x.FileAttachments.Select(x => new TenderFileAttachmentDetailsViewModel()
                    {
                        Id = x.Id,
                        FileName = x.FileName,
                        FilePath = x.FullPath,
                        ArchiveName = x.ArchiveName,
                    }).ToList()
                })
                .FirstOrDefault();

            if (tender == null)
                return NotFound();

            var attachments = tender.Attachments;

            var archiveItems = attachments.Where(x => x.ArchiveName != null).ToList();

            foreach (var archiveItem in archiveItems)
            {
                attachments.Remove(archiveItem);
            }

            foreach (var attachment in attachments)
            {
                attachment.NestedFiles = archiveItems
                    .Where(x => x.ArchiveName == attachment.FileName)
                    .ToList();
            }


            return View(tender);
        }

        public IActionResult Download(System.Guid id)
        {
            var attachment = dbContext.TenderFileAttachments.Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Content,
                    x.FileName 
                }).FirstOrDefault();

            if (attachment == null)
                return NotFound();

            return File(attachment.Content, "application/octet-stream", attachment.FileName);
        }

        public IActionResult FileDetails(System.Guid id)
        {
            var attachment = dbContext.TenderFileAttachments.Where(x => x.Id == id)
                .Select(x => new TenderFileAttachmentDetailsViewModel
                {
                    ExtractedText = x.ExtractedText,
                    FileName = x.FileName,
                    Id = x.Id
                })
                .FirstOrDefault();

            if (attachment == null)
                return NotFound();

            return View(attachment);
        }
    }
}
