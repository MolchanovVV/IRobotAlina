using IRobotAlina.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.Storage
{
    public class SaveTenderFileToFileSystemService : ISaveTenderFileAttachment
    {
        private readonly string fileFolder;

        public SaveTenderFileToFileSystemService(IWebHostEnvironment environment)
        {
            fileFolder = Path.Combine(environment.ContentRootPath, "Files");
            
            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);
        }

        public Task Save(TenderFileAttachment attachment)
        {
            if (attachment.Content == null)
                return Task.FromResult(0);

            var filePath = Path.Combine(fileFolder, attachment.TenderId.ToString(), attachment.FullPath.TrimStart('\\'));
            var directoryName = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            File.WriteAllBytes(filePath, attachment.Content);

            return Task.FromResult(0);
        }

        public Task Update(TenderFileAttachment attachment)
        {
            return Task.CompletedTask;
        }
    }
}
