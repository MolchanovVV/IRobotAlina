namespace IRobotAlina.Web.Models
{
    public class CheckConfigurationViewModel
    {
        public bool IsEWSWorking { get; set; }

        public string EWSErrorMessage { get; set; }

        public bool IsZakupkiAuthWorking { get; set; }
    }
}
