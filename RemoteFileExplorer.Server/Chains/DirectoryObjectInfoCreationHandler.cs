using System.IO;
using RemoteFileExplorer.Middleware.Data;
using RemoteFileExplorer.Server.Factories;

namespace RemoteFileExplorer.Server.Chains
{
    //Chain of responsibility
    public class DirectoryObjectInfoCreationHandler : FileSystemObjectInfoCreationHandler
    {
        public override FileSystemObjectInfo HandleCreation(string path)
        {
            if (!Directory.Exists(path))
                return Successor?.HandleCreation(path);

            var factory = new DirectoryObjectInfoFactory();
            return factory.Create(path);
        }
    }
}
