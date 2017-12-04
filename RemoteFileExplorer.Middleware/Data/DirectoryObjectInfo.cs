using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RemoteFileExplorer.Middleware.Data
{
    //Prototype
    //Composite
    [DataContract]
    public class DirectoryObjectInfo : FileSystemObjectInfo
    {
        [DataMember]
        private readonly List<FileSystemObjectInfo> _childObjects;

        public DirectoryObjectInfo()
        {
            _childObjects = new List<FileSystemObjectInfo>();
        }

        public override List<FileSystemObjectInfo> GetChildren()
        {
            return _childObjects;
        }

        public override void AddСhildObject(FileSystemObjectInfo obj)
        {
            _childObjects.Add(obj);
        }

        public override void RemoveСhildObject(FileSystemObjectInfo obj)
        {
            _childObjects.Remove(obj);
        }

        public override FileSystemObjectInfo Clone()
        {
            var clone = new DirectoryObjectInfo { Path = Path };
            _childObjects.ForEach(x => clone.AddСhildObject(x.Clone()));
            return clone;
        }
    }
}
