using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IRobotAlina.Web.Services.Mails
{
    public class ZakupkiKonturMailParser : IZakupkiKonturMailParser
    {
        public (string name, List<MailLink> links) GetLinks(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nameNodes = doc.DocumentNode.SelectNodes("//span[contains(text(), \"07:00\")]");
            var name = HttpUtility.HtmlDecode(string.Concat("Уведомления по закупкам ", nameNodes.Select(x => x.InnerText)?.FirstOrDefault().Replace("\r\n", " ") ?? string.Empty));

            List<MailLink> links = new List<MailLink>();

            try
            {
                var linkNodes = doc.DocumentNode.SelectNodes("//a[contains(@href, \"subscription\")] [not(contains(@href, \"Favorites\"))] [not(contains(@href, \"NewPurchases\"))]");
                if (linkNodes != null)
                {
                    links.AddRange(linkNodes
                        .Select(x => x.GetAttributeValue("href", null))
                       .Where(x => x != null).Select(s => new MailLink() { Type = EMailLinkType.Typical, Link = s })
                       .ToList());
                }

                //https://zakupki.kontur.ru/Favorites/Changes?utm_content=changes-link&utm_medium=email&utm_source=subscription
                var linkNode = doc.DocumentNode.SelectSingleNode("//a[contains(@href, \"Favorites\")]");
                if (linkNode != null)
                {
                    links.Add(new MailLink() { Type = EMailLinkType.Change, Link = linkNode.GetAttributeValue("href", null) });
                }

                //https://zakupki.kontur.ru/NewPurchases/DownloadExcel?from=2021-04-14T07%3A00%3A00%2B03%3A00&to=2021-04-14T14%3A00%3A00%2B03%3A00&utm_campaign=subscription&utm_content=templates-link&utm_medium=excel&utm_source=zakupki
                linkNode = doc.DocumentNode.SelectSingleNode("//a[contains(@href, \"DownloadExcel\")]");
                if (linkNode != null)
                {
                    links.Add(new MailLink() { Type = EMailLinkType.Download, Link = linkNode.GetAttributeValue("href", null) });
                }

                return (name, links);
            }
            catch
            {
                return (name, links);
            }            
        }
    }
}
