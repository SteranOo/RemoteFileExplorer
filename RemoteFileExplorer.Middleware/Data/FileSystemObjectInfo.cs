using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RemoteFileExplorer.Middleware.Data
{
    //Prototype
    //Composite
    [DataContract]
    [KnownType(typeof(DirectoryObjectInfo))]
    [KnownType(typeof(FileObjectInfo))]
    public abstract class FileSystemObjectInfo : IFileSystemObjectPrototype
    {
        [DataMember]
        public string Path { get; set; }

        public string Name => System.IO.Path.GetFileName(Path);

        public abstract List<FileSystemObjectInfo> GetChildren();

        public abstract void AddСhildObject(FileSystemObjectInfo obj);

        public abstract void RemoveСhildObject(FileSystemObjectInfo obj);

        public abstract FileSystemObjectInfo Clone();

        public override string ToString()
        {
            return Name;
        }
    }
}
