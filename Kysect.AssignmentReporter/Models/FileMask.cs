using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kysect.AssignmentReporter.Models
{
    public class FileMask
    {
        private readonly List<string> _names;
        private readonly List<string> _extensions;
        private readonly List<string> _directories;

        public FileMask(List<string> names, List<string> extensions, List<string> directories)
        {
            _names = names;
            _extensions = extensions;
            _directories = directories;
        }

        public FileMask()
        {
            _names = new List<string>();
            _extensions = new List<string>();
            _directories = new List<string>();
        }

        public bool NameIntersection(string fileName)
        {
            return _names.Contains(fileName);
        }

        public bool ExtensionIntersection(string fileFormat)
        {
            return _extensions.Contains(fileFormat);
        }

        public bool DirectoryIntersection(string path)
        {
            return path
                       .Split(Path.DirectorySeparatorChar)
                       .Where(s => _directories.Contains(s))
                       .ToList().Count != 0;
        }
    }
}