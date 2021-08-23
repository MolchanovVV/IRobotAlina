using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.Mails
{
    public class MailLink
    {
        public EMailLinkType Type { get; set; }
        public string Link { get; set; }

        public static List<MailLink> GetMailLinks(string links)
        {
            List<MailLink> result = new List<MailLink>();

            if (!string.IsNullOrWhiteSpace(links))
            {
                foreach (var item in links.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] values = item.Trim(new char[] { '[', ']' }).Split('~', StringSplitOptions.RemoveEmptyEntries);
                    
                    bool resParse = Enum.TryParse(values[0], out EMailLinkType type);

                    result.Add(new MailLink() { Type = resParse ? type : EMailLinkType.Unknown, Link = values[1] });
                }
            }
            
            return result;
        }
        
        public override string ToString()
        {
            return $"[{Type}~{Link}]";
        }
    }
}
