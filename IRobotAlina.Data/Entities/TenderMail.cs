using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRobotAlina.Data.Entities
{
    public class TenderMail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TenderPlatform")]
        [Required]
        [Column(TypeName = "nvarchar(36)")]
        public string TenderPlatformId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string UIDL { get; set; }
        
        public string Name { get; set; }

        public string Content { get; set; }

        public string HTMLBody { get; set; }

        public string InnerTextBody { get; set; }
        
        public System.DateTime SentDateTime { get; set; }

        public System.DateTime ReceiptDateTime { get; set; }

        public bool IsProcessed { get; set; }
        
        public virtual TenderPlatform TenderPlatform { get; set; }

        public virtual ICollection<Tender> Tenders { get; set; } = new List<Tender>();        
    }
}
