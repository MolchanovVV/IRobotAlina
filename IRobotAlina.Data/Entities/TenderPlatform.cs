using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRobotAlina.Data.Entities
{
    public class TenderPlatform
    {
        [Column(TypeName = "nvarchar(36)")]
        [Key]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<TenderMail> TenderMails { get; set; } = new List<TenderMail>();
    }
}
