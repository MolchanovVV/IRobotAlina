using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.Scraper
{
    public interface ITenderScraper
    {
        public Task<TenderDto> GetTenderInfo(string tenderUrl);

        /// <summary>
        /// Extracts file attachment urls for given tender url
        /// </summary>
        /// <param name="tenderUrl"></param>
        /// <returns></returns>
        public IEnumerable<TenderDocumentDto> GetDocumentUrls(string tenderUrl);

        /// <summary>
        /// Returns cookies to download file from environment with authentication
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cookie> GetCookies();
    }

    public interface IZakupkiKonturScraper : ITenderScraper
    {
        //public Task<bool> DownloadFileTendersAdditionalPart(string url);
        public void DownloadFileTendersAdditionalPart(string url);
    }
}
