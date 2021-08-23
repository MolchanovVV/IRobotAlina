using System.ComponentModel.DataAnnotations;

namespace IRobotAlina.Data.Entities
{
    public class ConfigurationItem
    {
        [Key]
        public EConfigurationItemType Type { get; set; }

        public string Value { get; set; }
    }
}
