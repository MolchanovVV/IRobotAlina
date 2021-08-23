using System.Collections.Generic;

namespace IRobotAlina.Web.Services.Scraper
{
    public class TenderDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<TenderDocumentDto> Documents { get; set; }
    }
}
