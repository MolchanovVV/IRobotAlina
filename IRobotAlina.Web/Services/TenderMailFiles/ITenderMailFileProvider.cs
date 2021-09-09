using IRobotAlina.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.TenderMailFiles
{
    public interface ITenderMailFileProvider
    {
        public List<TenderMailFile> GetTenderMailFiles(int mailId);
    }
}
