using System.ComponentModel;

namespace IRobotAlina.Web.Models
{
    public class ConfigurationViewModel
    {
        [DisplayName("IP")]
        public string EWSIP { get; set; }
        [DisplayName("Пользователь")]
        public string EWSLogin { get; set; }
        [DisplayName("Пароль")]
        public string EWSPassword { get; set; }

        [DisplayName("Логин")]
        public string ZakupkiEmail { get; set; }
        [DisplayName("Пароль")]
        public string ZakupkiPassword { get; set; }

        [DisplayName("Имейлы отправителей")]
        public string MailHostNames { get; set; }
    }
}
