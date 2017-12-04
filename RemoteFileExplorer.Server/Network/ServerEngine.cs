using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using RemoteFileExplorer.Middleware.Data;
using RemoteFileExplorer.Middleware.Network;
using RemoteFileExplorer.Server.Services;

namespace RemoteFileExplorer.Server.Network
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServerEngine : IServerEngine
    {
        private OperationResult MakeOperation(Action operation)
        {
            try
            {
                operation();
                return new OperationResult { Success = true };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        private OperationResult<T> MakeOperation<T>(Func<T> operation)
        {
            try
            {
                var result = operation();
                return new OperationResult<T> { Success = true, Response = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<T> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public OperationResult Connect()
        {
            return new OperationResult { Success = true };
        }

        public OperationResult<List<string>> GetLogicalDrives()
        {
            var drives = FileExplorerService.Instance.GetLogicalDrives();
            return new OperationResult<List<string>> { Success = true, Response = drives };
        }

        public OperationResult<FileSystemObjectInfo> GetFileSystemObject(string path)
        {
            return MakeOperation(() => FileExplorerService.Instance.GetFileSystemObject(path));
        }

        public OperationResult DeleteFile(string path)
        {
            return MakeOperation(() => FileExplorerService.Instance.DeleteFile(path));
        }

        public OperationResult DeleteDirectory(string path, bool recursive)
        {
            return MakeOperation(() => FileExplorerService.Instance.DeleteDirectory(path, recursive));
        }

        public OperationResult MoveFile(string src, string dist)
        {
            return MakeOperation(() => FileExplorerService.Instance.MoveFile(src, dist));
        }

        public OperationResult MoveDirectory(string src, string dist)
        {
            return MakeOperation(() => FileExplorerService.Instance.MoveDirectory(src, dist));
        }

        public OperationResult CopyFile(string src, string dist, bool overwrite)
        {
            return MakeOperation(() => FileExplorerService.Instance.CopyFile(src, dist, overwrite));
        }

        public OperationResult CopyDirectory(string src, string dist, bool copySubDirs, bool overwrite)
        {
            return MakeOperation(() => FileExplorerService.Instance.CopyDirectory(src, dist, copySubDirs, overwrite));
        }
    }
}
