using IRobotAlina.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.Builder
{
    public class ZakupkiKonturTenderBuilder : ITenderBuilder, IZakupkiKonturTenderBuilder
    {
        private readonly ApplicationDbContext dbContext;

        public ZakupkiKonturTenderBuilder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public (Tender tender, List<LinkFileDto> rootFiles) GetOrCreate(Tender info)
        {
            string externalId = ExtractExternalId(info.Url);

            var tender = dbContext.Tenders.FirstOrDefault(x => x.ExternalId == externalId && x.TenderMailId == info.TenderMailId);

            if (tender != null)
            {
                var files = dbContext.TenderFileAttachments
                    .Where(x => x.TenderId == tender.Id && x.ArchiveName == null)
                    .Select(x => new LinkFileDto()
                    {
                        Name = x.FileName
                    })
                    .ToList();

                return (tender, files);
            }

            tender = new Tender()
            {
                ExternalId = externalId,
                Url = info.Url,
                Name = info.Name,
                TenderMailId = info.TenderMailId,
                Order = info.Order,
                Description = info.Description
            };

            dbContext.Add(tender);
            dbContext.SaveChanges();
            
            return (tender, new List<LinkFileDto>());
        }

        // link example "https://zakupki.kontur.ru/2487205_4?utm_source=zakupki&utm_campaign=subscription&utm_medium=email&utm_content=7722743350"
        private string ExtractExternalId(string url)
        {
            var endOfLinkIndex = url.LastIndexOf('/') + 1;
            var startOfQueryIndex = url.IndexOf('?');
            if (startOfQueryIndex == -1)
                return url[endOfLinkIndex..]; // no query
            else
                return url[endOfLinkIndex..startOfQueryIndex]; // 2487205_4

        }
    }
}
