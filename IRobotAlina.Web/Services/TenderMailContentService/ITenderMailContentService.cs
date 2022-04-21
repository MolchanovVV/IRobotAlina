using IRobotAlina.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.TenderMailContentService
{
    public interface ITenderMailContentService
    {
        public void CreateTenderMailContent(List<int> mailIds);
    }
}
