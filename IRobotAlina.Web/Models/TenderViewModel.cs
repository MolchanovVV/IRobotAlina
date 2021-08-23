using System.Collections.Generic;

namespace IRobotAlina.Web.Models
{
    public class TenderViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int FileCount { get; set; }

        public string PlatformName { get; set; }

        public int? Order { get; set; }

        public int? MailId { get; set; }

        public string MailName { get; set; }
    }

    public class TenderDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ExternalId { get; set; }

        public string Url { get; set; }
        
        public List<TenderFileAttachmentDetailsViewModel> Attachments { get; set; }
    }    
}