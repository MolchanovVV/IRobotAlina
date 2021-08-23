using System.Collections.Generic;

namespace IRobotAlina.Web.Models
{
    public class HomeViewModel
    {
        public List<JobItem> Jobs { get; set; }

        public bool AreAllConfigurationsValid { get; set; }

        public BasePaginationModel<TenderMailViewModel> TenderMails { get; set; }

        public int TotalTendersCount { get; set; }
    }

    public class JobItem
    {
        public string Name { get; set; }

        public string Status { get; set; }
    }
}
