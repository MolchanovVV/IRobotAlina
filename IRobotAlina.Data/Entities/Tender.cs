using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRobotAlina.Data.Entities
{
    public class Tender
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TenderMail")]
        [Required]
        public int TenderMailId { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
       
        public string Description { get; set; }
                                
        [Required]
        public int Order { get; set; }

        public virtual TenderMail TenderMail { get; set; }

        public virtual ICollection<TenderFileAttachment> FileAttachments { get; set; }
    }
}
