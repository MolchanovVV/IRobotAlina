using System;
using System.IO;

namespace IRobotAlina.Web.Services.Storage
{
    public class TemporaryStoragePathProvider : ITemporaryStoragePathProvider, IDisposable
    {
        private readonly string folder;

        public TemporaryStoragePathProvider()
        {
            folder = Path.Combine(Path.GetTempPath(), $"IRobotAlina-{Guid.NewGuid()}");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        public void Dispose()
        {
            try
            {
                Directory.Delete(folder, true);
            }
            finally
            { }
        }

        public string GetTempFolderPath()
        {
            return folder;
        }
    }
}
