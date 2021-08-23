using IRobotAlina.Data.Entities;
using IRobotAlina.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services
{
    public interface ITenderMailProvider
    {
        public Task<List<TenderMailDto>> GetTenderMails();

        public void MarkAsCompleted(List<TenderMailDto> tenderMails);

        public void MarkAsCompleted(TenderMailDto tenderMail);
    }

    public interface IZakupkiKonturTenderMailProvider : ITenderMailProvider
    { }
}
