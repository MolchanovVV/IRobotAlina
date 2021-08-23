namespace IRobotAlina.Web.Models
{

    public class TenderPaginationModel : BasePaginationModel<TenderViewModel>
    {
        public int TotalFileCount { get; set; }

        public string MailName { get; set; }

        public int? MailId { get; set; }
    }
}
