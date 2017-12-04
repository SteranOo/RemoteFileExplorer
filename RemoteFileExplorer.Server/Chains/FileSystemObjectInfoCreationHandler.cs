using RemoteFileExplorer.Middleware.Data;

namespace RemoteFileExplorer.Server.Chains
{
    //Chain of responsibility
    public abstract class FileSystemObjectInfoCreationHandler
    {
        public FileSystemObjectInfoCreationHandler Successor { get; set; }

        public abstract FileSystemObjectInfo HandleCreation(string path);
    }
}
