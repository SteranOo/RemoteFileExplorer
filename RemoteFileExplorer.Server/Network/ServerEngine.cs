using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using RemoteFileExplorer.Middleware.Network;
using RemoteFileExplorer.Server.Services;

namespace RemoteFileExplorer.Server.Network
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    class ServerEngine : IServerEngine
    {
        public void Connect()
        {
            Console.WriteLine("Connected");
        }

        public List<string> GetLogicalDrives()
        {
            return FileExplorerService.Instance.GetLogicalDrives();
        }

        public List<DirectoryInfo> GetSubdirectories(string path)
        {
            return FileExplorerService.Instance.GetSubdirectories(path);
        }

        public List<FileInfo> GetFiles(string path)
        {
            return FileExplorerService.Instance.GetFiles(path);
        }

        public void DeleteFile(string path)
        {
            FileExplorerService.Instance.DeleteFile(path);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            FileExplorerService.Instance.DeleteDirectory(path, recursive);
        }

        public void MoveFile(string src, string dist)
        {
            FileExplorerService.Instance.MoveFile(src, dist);
        }

        public void MoveDirectory(string src, string dist)
        {
            FileExplorerService.Instance.MoveDirectory(src, dist);
        }

        public void CopyFile(string src, string dist, bool overwrite)
        {
            FileExplorerService.Instance.CopyFile(src, dist, overwrite);
        }

        public void CopyDirectory(string src, string dist, bool copySubDirs, bool overwrite)
        {
            FileExplorerService.Instance.CopyDirectory(src, dist, copySubDirs, overwrite);
        }
    }
}
