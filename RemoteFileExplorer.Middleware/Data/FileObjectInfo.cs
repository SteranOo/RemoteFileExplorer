using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RemoteFileExplorer.Middleware.Data
{
    //Prototype
    //Composite
    [DataContract]
    public class FileObjectInfo : FileSystemObjectInfo
    {
        public override FileSystemObjectInfo Clone()
        {
            return new FileObjectInfo { Path = Path };
        }

        public override List<FileSystemObjectInfo> GetChildren()
        {
            throw new InvalidOperationException();
        }

        public override void AddСhildObject(FileSystemObjectInfo obj)
        {
            throw new InvalidOperationException();
        }

        public override void RemoveСhildObject(FileSystemObjectInfo obj)
        {
            throw new InvalidOperationException();
        }
    }
}
