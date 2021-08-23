namespace IRobotAlina.Web.Services.Files
{
    public class FileItem
    {
        public string FileName { get; set; }

        public string ArchiveName { get; set; }

        public string FilePath { get; set; }

        public byte[] FileContent { get; set; }
    }
}
