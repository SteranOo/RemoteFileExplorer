using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace RemoteFileExplorer.Middleware.Network
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IServerEngine
    {
        [OperationContract]
        void Connect();

        [OperationContract]
        List<string> GetLogicalDrives();

        [OperationContract]
        List<DirectoryInfo> GetSubdirectories(string path);

        [OperationContract]
        List<FileInfo> GetFiles(string path);

        [OperationContract]
        void DeleteFile(string path);

        [OperationContract]
        void DeleteDirectory(string path, bool recursive);

        [OperationContract]
        void MoveFile(string src, string dist);

        [OperationContract]
        void MoveDirectory(string src, string dist);

        [OperationContract]
        void CopyFile(string src, string dist, bool overwrite);

        [OperationContract]
        void CopyDirectory(string src, string dist, bool copySubDirs, bool overwrite);
    }
}
