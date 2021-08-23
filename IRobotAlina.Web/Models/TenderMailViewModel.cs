namespace IRobotAlina.Web.Models
{
    public class TenderMailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public System.DateTime SentDateTime { get; set; }

        public bool IsProcessed { get; set; }

        public int TenderCount { get; set; }
    }
}
