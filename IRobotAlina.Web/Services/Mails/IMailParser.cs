using System.Collections.Generic;

namespace IRobotAlina.Web.Services.Mails
{
    public interface IZakupkiKonturMailParser
    {
        public (string name, List<MailLink> links) GetLinks(string html);
    }
}
