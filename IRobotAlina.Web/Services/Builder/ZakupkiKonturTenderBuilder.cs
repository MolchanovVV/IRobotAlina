using IRobotAlina.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Services.Builder
{
    public class ZakupkiKonturTenderBuilder : IZakupkiKonturTenderBuilder
    {
        private readonly ApplicationDbContext dbContext;

        public ZakupkiKonturTenderBuilder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public (Tender tender, List<LinkFileDto> rootFiles) GetOrCreate(Tender info, Tender addPart)
        {
            string externalId = ExtractExternalId(info.Url); 

            var tender = dbContext.Tenders.FirstOrDefault(x => x.TenderMailId == info.TenderMailId && x.ExternalId == externalId);

            if (tender != null)
            {
                var files = dbContext.TenderFileAttachments
                    .Where(x => x.TenderId == tender.Id && string.IsNullOrEmpty(x.ArchiveName)) // отбираются только архивы и файлы, которые не были в архивах. Т.е. именно тот набор файлов, который был в тендере изначально.
                    .Select(x => new LinkFileDto()
                    {
                        Name = x.FileName
                    })
                    .ToList();

                return (tender, files);
            }

            if (addPart != null && (info.Number?.ToLowerInvariant() ?? "") == addPart.Number.ToLowerInvariant())
            {
                tender = addPart;
            }
            else
            {
                tender = new Tender();
            }

            tender.Id = 0;
            tender.TenderMailId = info.TenderMailId;            
            tender.ExternalId = externalId;
            tender.Number = info.Number;
            tender.Name = info.Name;
            tender.Url = info.Url;
            tender.Order = info.Order;
            tender.Description = info.Description;
            tender.Status = ETenderStatus.New;

            try
            {
                dbContext.Add(tender);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"DbUpdateException: {ex.Message}, inner exceptionMessage: {ex.InnerException} (TenderMailId: [{tender.TenderMailId}], Name: [{tender.Name}], Order: [{tender.Order}])");
            }
            catch(Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exceptionMessage: {ex.InnerException} (TenderMailId: [{tender.TenderMailId}], Name: [{tender.Name}], Order: [{tender.Order}])");
            }
                        
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
