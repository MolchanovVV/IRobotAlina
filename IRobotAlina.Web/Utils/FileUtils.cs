using System.IO;

namespace IRobotAlina.Web.Utils
{
    public class FileUtils
    {
        public static bool IsFileArchive(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension == ".7z" || extension == ".zip" || extension == ".rar";
        }
    }
}
