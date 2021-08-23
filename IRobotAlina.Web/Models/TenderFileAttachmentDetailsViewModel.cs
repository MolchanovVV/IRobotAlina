using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Models
{
    public class TenderFileAttachmentDetailsViewModel
    {
        public System.Guid Id { get; set; }

        public string FileName { get; set; }

        public string ArchiveName { get; set; }

        public string ExtractedText { get; set; }

        public string FilePath { get; set; }

        public List<TenderFileAttachmentDetailsViewModel> NestedFiles { get; set; }
            = new List<TenderFileAttachmentDetailsViewModel>();
    }
}
