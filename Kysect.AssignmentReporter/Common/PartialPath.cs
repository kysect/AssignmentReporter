using System;

namespace Kysect.AssignmentReporter.Common
{
    public class PartialPath
    {
        public string Root { get; }
        public string Path { get; }

        public PartialPath(string root, string fullPath)
        {
            if (!string.IsNullOrWhiteSpace(root) && !fullPath.StartsWith(root))
                throw new ArgumentException("Full path should start with root path");

            Root = root;

            if (root == fullPath)
                Path = string.Empty;
            else if (string.IsNullOrWhiteSpace(root))
                Path = fullPath;
            else
            {
                Path = fullPath.Substring(root.Length);
                if (Path[0] == System.IO.Path.DirectorySeparatorChar)
                    Path = Path.Remove(0, 1);
            }
        }

        public static PartialPath FromRoot(string fullPath) => new PartialPath(string.Empty, fullPath);

        public override string ToString()
        {
            return Path;
        }
    }
}