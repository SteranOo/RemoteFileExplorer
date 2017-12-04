using System.IO;
using RemoteFileExplorer.Middleware.Data;
using RemoteFileExplorer.Server.Factories;

namespace RemoteFileExplorer.Server.Chains
{
    //Chain of responsibility
    public class FileObjectInfoCreationHandler : FileSystemObjectInfoCreationHandler
    {
        public override FileSystemObjectInfo HandleCreation(string path)
        {
            if (!File.Exists(path))
                return Successor?.HandleCreation(path);

            var factory = new FileObjectInfoFactory();
            return factory.Create(path);
        }
    }
}
