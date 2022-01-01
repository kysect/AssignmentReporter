using System.IO;

namespace Kysect.AssignmentReporter.Common
{
    public class PartialFilePath
    {
        private PartialPath _partialPath;

        public FileInfo File { get; }
        public PartialPath ParentDirectoryPath { get; }

        public PartialFilePath(string root, string fileFullPath)
        {
            File = new FileInfo(fileFullPath);
            ParentDirectoryPath = new PartialPath(root, File.DirectoryName);
            _partialPath = new PartialPath(root, fileFullPath);
        }

        public override string ToString()
        {
            return _partialPath.ToString();
        }
    }
}