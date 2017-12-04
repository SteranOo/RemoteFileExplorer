using System.IO;

namespace RemoteFileExplorer.Server.Services
{
    public class FileExplorerMoveService
    {
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
    }
}
