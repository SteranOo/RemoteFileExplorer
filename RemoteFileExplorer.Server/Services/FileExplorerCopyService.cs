using System.IO;

namespace RemoteFileExplorer.Server.Services
{
    public class FileExplorerCopyService
    {
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
