using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RemoteFileExplorer.Server.Services
{
    //singleton
    public class FileExplorerService
    {
        private static FileExplorerService _instance;

        public static FileExplorerService Instance => _instance ?? (_instance = new FileExplorerService());

        protected FileExplorerService() { }

        public List<string> GetLogicalDrives()
        {
            return DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed || x.DriveType == DriveType.Removable)
                .Select(x => x.Name)
                .ToList();
        }

        public List<DirectoryInfo> GetSubdirectories(string path)
        {
            if (Directory.Exists(path))
            {
                return new DirectoryInfo(path).GetDirectories().ToList();
            }
            return null;
        }

        public List<FileInfo> GetFiles(string path)
        {
            if (Directory.Exists(path))
            {
                return new DirectoryInfo(path).GetFiles().ToList();
            }
            return null;
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }
        }

        public void MoveFile(string src, string dist)
        {
            if (File.Exists(src))
            {
                File.Move(src, dist);
            }
        }

        public void MoveDirectory(string src, string dist)
        {
            if (Directory.Exists(src))
            {
                Directory.Move(src, dist);
            }
        }

        public void CopyFile(string src, string dist, bool overwrite)
        {
            if (File.Exists(src))
            {
                File.Copy(src, dist, overwrite);
            }
        }

        public void CopyDirectory(string src, string dist, bool copySubDirs, bool overwrite)
        {
            var dir = new DirectoryInfo(src);
            var dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                return;
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(dist))
            {
                Directory.CreateDirectory(dist);
            }

            // Get the file contents of the directory to copy.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                // Create the path to the new copy of the file.
                var path = Path.Combine(dist, file.Name);

                // Copy the file.
                file.CopyTo(path, overwrite);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {
                foreach (var subdir in dirs)
                {
                    // Create the subdirectory.
                    var temppath = Path.Combine(dist, subdir.Name);

                    // Copy the subdirectories.
                    CopyDirectory(subdir.FullName, temppath, true, overwrite);
                }
            }
        }
    }
}
