using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using RemoteFileExplorer.Middleware.Data;

namespace RemoteFileExplorer.Middleware.Network
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IServerEngine
    {
        [OperationContract]
        OperationResult Connect();

        [OperationContract]
        OperationResult<List<string>> GetLogicalDrives();

        [OperationContract]
        OperationResult<FileSystemObjectInfo> GetFileSystemObject(string path);

        [OperationContract]
        OperationResult DeleteFile(string path);

        [OperationContract]
        OperationResult DeleteDirectory(string path, bool recursive);

        [OperationContract]
        OperationResult MoveFile(string src, string dist);

        [OperationContract]
        OperationResult MoveDirectory(string src, string dist);

        [OperationContract]
        OperationResult CopyFile(string src, string dist, bool overwrite);

        [OperationContract]
        OperationResult CopyDirectory(string src, string dist, bool copySubDirs, bool overwrite);
    }
}
