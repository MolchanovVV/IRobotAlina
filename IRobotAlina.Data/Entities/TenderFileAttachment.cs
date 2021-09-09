using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRobotAlina.Data.Entities
{

    public class TenderFileAttachment : IAttachment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Guid Id { get; set; }

        [ForeignKey("Tender")]
        public int TenderId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string FileName { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string FullPath { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string ArchiveName { get; set; }

        public string ExtractedText { get; set; }
        
        public string ExceptionMessage { get; set; }

        public byte[] Content { get; set; }

        public bool IsArchive { get; set; }

        public ETenderFileAttachmentStatus Status { get; set; }

        public virtual Tender Tender { get; set; }

        public TenderFileAttachment()
        {
            Status = ETenderFileAttachmentStatus.Unknown;
        }
    }
}
