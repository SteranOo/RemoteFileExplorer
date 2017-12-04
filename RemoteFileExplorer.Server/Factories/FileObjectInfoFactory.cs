using System.IO;
using RemoteFileExplorer.Middleware.Data;

namespace RemoteFileExplorer.Server.Factories
{
    //Factory method
    public class FileObjectInfoFactory : FileSystemObjectInfoFactory
    {
        public override FileSystemObjectInfo Create(string path)
        {
            if (!File.Exists(path))
                return null;

            return new FileObjectInfo { Path = path };
        }
    }
}

