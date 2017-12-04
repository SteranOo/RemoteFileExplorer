using System.Collections.Generic;
using System.IO;
using System.Linq;
using RemoteFileExplorer.Middleware.Data;
using RemoteFileExplorer.Server.Chains;

namespace RemoteFileExplorer.Server.Services
{
    public class FileExplorerGetInfoService
    {
        public List<string> GetLogicalDrives()
        {
            return DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed || x.DriveType == DriveType.Removable)
                .Select(x => x.Name)
                .ToList();
        }

        public FileSystemObjectInfo GetFileSystemObject(string path)
        {
            var dirCreationHandler = new DirectoryObjectInfoCreationHandler();
            var fileCreationHandler = new FileObjectInfoCreationHandler();
            dirCreationHandler.Successor = fileCreationHandler;
            return dirCreationHandler.HandleCreation(path);
        }
    }
}
