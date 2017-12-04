using RemoteFileExplorer.Middleware.Data;

namespace RemoteFileExplorer.Server.Factories
{
    //Factory method
    public abstract class FileSystemObjectInfoFactory
    {
        public abstract FileSystemObjectInfo Create(string path);
    }
}
