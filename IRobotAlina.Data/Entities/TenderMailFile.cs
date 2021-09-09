using IRobotAlina.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IRobotAlina.Data.Entities
{
    public class TenderMailFile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("TenderMail")]
        [Required]
        public int TenderMailId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string ParsedData { get; set; }

        [Required]
        public ETenderMailFileType Type { get; set; }

        [Required]
        public ETenderMailFileStatus Status { get; set; }

        [Required]
        public DateTime? DateParsing { get; set; }

        public virtual TenderMail TenderMail { get; set; }

        public TenderMailFile()
        {
            Type = ETenderMailFileType.Unknown;
            Status = ETenderMailFileStatus.Unknown;
        }
    }
}
