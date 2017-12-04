using System.IO;
using RemoteFileExplorer.Middleware.Data;

namespace RemoteFileExplorer.Server.Factories
{
    //Factory method
    public class DirectoryObjectInfoFactory : FileSystemObjectInfoFactory
    {
        public override FileSystemObjectInfo Create(string path)
        {
            var directoryInfo = new DirectoryObjectInfo { Path = path };
            var subdirectories = Directory.GetDirectories(path);
            foreach (var subdirectory in subdirectories)
            {
                directoryInfo.AddСhildObject(new DirectoryObjectInfo { Path = subdirectory });
            }
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                directoryInfo.AddСhildObject(new FileObjectInfo { Path = file });
            }
            return directoryInfo;
        }
    }
}
