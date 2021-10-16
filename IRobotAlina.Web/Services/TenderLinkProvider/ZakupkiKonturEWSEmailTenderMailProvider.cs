using IRobotAlina.Data.Entities;
using IRobotAlina.Data.Seed;
using IRobotAlina.Web.Models;
using IRobotAlina.Web.Services.Mails;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.TenderLinkProvider
{
    public class ZakupkiKonturEWSEmailTenderMailProvider : IZakupkiKonturTenderMailProvider
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly EWSMailProvider mailProvider;
        private readonly IZakupkiKonturMailParser mailParser;

        public ZakupkiKonturEWSEmailTenderMailProvider(ApplicationDbContext applicationDbContext,
            EWSMailProvider mailProvider,
            IZakupkiKonturMailParser mailParser)
        {
            this.applicationDbContext = applicationDbContext;
            this.mailProvider = mailProvider;
            this.mailParser = mailParser;
        }

        public async Task<List<TenderMailDto>> GetTenderMails()
        {
            SaveMailsToDb();
            return await PrepareUnprocessedEmails();
        }

        private void SaveMailsToDb()
        {
            try
            {
                var mails = mailProvider.GetEmails().ToList();

                foreach (var item in mails)
                {
                    var (name, links) = mailParser.GetLinks(item.Mail.HtmlBody);
                    var content = string.Join(",", links.Select(s => s.ToString()));

                    /// -Молчанов 25.08.2021 Уже не первый раз натыкаюсь на ситуацию, когда у разных писем одинаковый UIDL. Как такое может быть?..
                    //if (!applicationDbContext.TenderMails.Any(x => x.UIDL == item.UIDL || x.Name == name))
                    if (!applicationDbContext.TenderMails.Any(x => x.Name == name))
                    {
                        var tenderMail = new TenderMail()
                        {
                            TenderPlatformId = TenderPlatforms.ZakupkiId,
                            UIDL = item.UIDL,
                            Name = name,
                            Content = content,
                            HTMLBody = item.Mail.HtmlBody,
                            InnerTextBody = item.Mail.TextBody,
                            SentDateTime = item.Mail.SentDate,
                            ReceiptDateTime = item.Mail.ReceivedDate,
                            IsProcessed = false
                        };

                        applicationDbContext.TenderMails.Add(tenderMail);
                    }

                    applicationDbContext.SaveChanges();
                }

                foreach (var item in mails)
                {
                    mailProvider.MarkAsProcessed(item.UIDL);
                }
            }
            catch { }
        }

        public Task<List<TenderMailDto>> PrepareUnprocessedEmails()
        {
            var unprocessedEmailsContent = applicationDbContext.TenderMails
                .Where(x => x.IsProcessed == false)
                .Select(x => new { x.Id, x.Content })
                .ToList();

            var result = unprocessedEmailsContent.Select(x => new TenderMailDto()
            {
                Id = x.Id,
                Links = MailLink.GetMailLinks(x.Content)
            }).ToList();

            return Task.FromResult(result);
        }

        public void MarkAsCompleted(List<TenderMailDto> tenderMails)
        {
            var unprocessedEmails = applicationDbContext.TenderMails
                .Where(x => x.IsProcessed == false)
                .ToList();

            foreach (var tenderMail in tenderMails.Where(x => x.Id.HasValue))
            {
                var unprocessedEmail = unprocessedEmails.FirstOrDefault(x => x.Id == tenderMail.Id);

                if (unprocessedEmail == null)
                {
                    continue;
                }

                unprocessedEmail.IsProcessed = true;

                applicationDbContext.Update(unprocessedEmail);
            }

            applicationDbContext.SaveChanges();
        }

        public void MarkAsCompleted(TenderMailDto tenderMailDto)
        {         
            var tenderMail = applicationDbContext.TenderMails?.FirstOrDefault(x => x.Id == tenderMailDto.Id);

            if (tenderMail == null)
            {
                return;
            }

            tenderMail.IsProcessed = true;            
            applicationDbContext.SaveChanges(); 
            applicationDbContext.Update(tenderMail);            
        }
    }
}
