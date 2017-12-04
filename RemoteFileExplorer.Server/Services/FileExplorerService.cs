using System.Collections.Generic;
using RemoteFileExplorer.Middleware.Data;

namespace RemoteFileExplorer.Server.Services
{
    //Singleton
    //Facade
    public class FileExplorerService
    {
        private static FileExplorerService _instance;

        public static FileExplorerService Instance => _instance ?? (_instance = new FileExplorerService());

        private readonly FileExplorerGetInfoService _getInfoService;

        private readonly FileExplorerCopyService _copyService;

        private readonly FileExplorerMoveService _moveService;

        private readonly FileExplorerDeleteService _deleteService;

        protected FileExplorerService()
        {
            _getInfoService = new FileExplorerGetInfoService();
            _copyService = new FileExplorerCopyService();
            _moveService = new FileExplorerMoveService();
            _deleteService = new FileExplorerDeleteService();
        }

        public List<string> GetLogicalDrives()
        {
            return _getInfoService.GetLogicalDrives();
        }

        public FileSystemObjectInfo GetFileSystemObject(string path)
        {
            return _getInfoService.GetFileSystemObject(path);
        }

        public void DeleteFile(string path)
        {
            _deleteService.DeleteFile(path);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            _deleteService.DeleteDirectory(path, recursive);
        }

        public void MoveFile(string src, string dist)
        {
            _moveService.MoveFile(src, dist);
        }

        public void MoveDirectory(string src, string dist)
        {
            _moveService.MoveDirectory(src, dist);
        }

        public void CopyFile(string src, string dist, bool overwrite)
        {
            _copyService.CopyFile(src, dist, overwrite);
        }

        public void CopyDirectory(string src, string dist, bool copySubDirs, bool overwrite)
        {
            _copyService.CopyDirectory(src, dist, copySubDirs ,overwrite);
        }
    }
}
