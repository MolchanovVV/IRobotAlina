using IRobotAlina.Web.Services.Storage;
using IRobotAlina.Web.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IRobotAlina.Web.Services.Files
{
    public class FileService : IDisposable
    {
        public void Dispose()
        {
            var tempPath = Path.GetTempPath();
            foreach (var directory in Directory.GetDirectories(tempPath)
                .Select(x => new DirectoryInfo(x)).Where(x => x.Name.StartsWith("IRobotAlina")))
            {
                try
                {
                    Directory.Delete(directory.FullName, true);
                }
                catch { }
            }
        }

        public IEnumerable<FileItem> ExtractFiles(string fileName, byte[] bytes)
        {
            if (FileUtils.IsFileArchive(fileName))
            {
                foreach (var file in ExtractArchiveFiles(fileName, bytes)) 
                {
                    file.ArchiveName = fileName;

                    yield return file;
                }
            }

            yield return new FileItem()
            {
                FileContent = bytes,
                FileName = fileName
            };
        }

        private IEnumerable<FileItem> ExtractArchiveFiles(string fileName, byte[] bytes)
        {
            string archivePathFile = null, archivatorPath = null, arguments = null, destination;

            try
            {
                archivePathFile = Path.GetTempFileName();
                var tempStoragePath = new TemporaryStoragePathProvider();                
                destination = Path.Combine(Path.GetDirectoryName(archivePathFile), tempStoragePath.GetTempFolderPath());

                Directory.CreateDirectory(destination);

                File.WriteAllBytes(archivePathFile, bytes);
                
                try
                {
                    if (Path.GetExtension(fileName).ToLowerInvariant() == ".7z")
                    {
                        archivatorPath = "./ExternalDLLs/7za.exe";
                        arguments = string.Format("x \"{0}\" -y -o\"{1}\"", archivePathFile, destination);
                    }
                    else
                    {
                        archivatorPath = "./ExternalDLLs/WinRAR.exe";
                        arguments = string.Format("x -y -ibck \"{0}\" \"{1}\"", archivePathFile, destination);
                    };

                    ProcessStartInfo pro = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        //WindowStyle = ProcessWindowStyle.Normal,
                        FileName = archivatorPath,
                        Arguments = arguments
                    };

                    Process archProc = Process.Start(pro);                    
                    archProc.WaitForExit();
                }
                catch (Exception) { }
            }
            finally
            {
                try
                {
                    if (File.Exists(archivePathFile))
                        File.Delete(archivePathFile);
                }
                catch { }
            }

            //var exts = new[] { ".rar", ".zip", ".7z" };
            foreach (var unzippedFile in Directory.GetFiles(destination, "*.*", SearchOption.AllDirectories)
                                                  //.Where(file => !exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase)))
                                                  )
            {
                
                using var fileStream = File.OpenRead(unzippedFile);
                using var ms = new MemoryStream();
                fileStream.CopyTo(ms);
               
                var relativePath = unzippedFile.Replace(destination, "", StringComparison.OrdinalIgnoreCase).TrimStart('\\');

                yield return new FileItem()
                {
                    FilePath = relativePath,
                    FileName = Path.GetFileName(relativePath),
                    FileContent = ms.ToArray()
                };                                
            }
        }
    }
}
