using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace IRobotAlina.Data.Entities
{
    public class TenderFileAttachment : IAttachment
    {
        private readonly int maxFileNameLength = 255;
        private readonly int maxFullPathLength = 512;
        private readonly int maxArchiveNameLength = 255;

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

        public TenderFileAttachment(int tenderId, string fileName, string fullPath, string archiveName, byte[] content, bool isArchive)
        {
            TenderId = tenderId;

            string _fileName = fileName.Replace(":", ""); // вы не поверите..
            if (_fileName?.Length > maxFileNameLength) // 02.02.2022, поймал наим.файла в 347 символов, а поле в базе 255, и расширять его в угоду всяким *censored* не хочу.
            {
                string extension = Path.GetExtension(_fileName);
                int maxLength = maxFileNameLength - (extension?.Length ?? 0);
                _fileName = string.Concat(_fileName.Substring(0, maxLength), extension);
            }
            FileName = _fileName;

            string _fullPath = fullPath;
            if (_fullPath?.Length > maxFullPathLength)
            {
                _fullPath = _fullPath.Substring(0, maxFullPathLength) ?? null;
            }
            FullPath = _fullPath;

            string _archiveName = archiveName;
            if (_archiveName?.Length > maxArchiveNameLength)
            {
                string extension = Path.GetExtension(_archiveName);
                int maxLength = maxArchiveNameLength - (extension?.Length ?? 0);
                _archiveName = string.Concat(_archiveName?.Substring(0, maxLength), extension);
            }
            ArchiveName = _archiveName;

            Content = content;
            IsArchive = isArchive;
            Status = ETenderFileAttachmentStatus.Unknown;
        }
    }
}
