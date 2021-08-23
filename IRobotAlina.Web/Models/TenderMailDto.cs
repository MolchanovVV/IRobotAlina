using IRobotAlina.Web.Services.Mails;
using System.Collections.Generic;

namespace IRobotAlina.Web.Models
{
    public class TenderMailDto
    {
        public int? Id { get; set; }
        
        public List<MailLink> Links { get; set; } = new List<MailLink>();
    }
}
