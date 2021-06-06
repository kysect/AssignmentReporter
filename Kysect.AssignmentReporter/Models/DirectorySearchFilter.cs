using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class DirectorySearchFilter
    {
        public List<string> UnacceptableDirectoryNames { get; set; }
        public string RegularExpression { get; set; } = string.Empty;
        
        public DirectorySearchFilter(List<string> unacceptableDirectoryNames, string regularExpression)
        {
            UnacceptableDirectoryNames = unacceptableDirectoryNames;
            RegularExpression = regularExpression;
        }
        
        public DirectorySearchFilter(string regularExpression) : this()
        {
            RegularExpression = regularExpression;
        }

        public DirectorySearchFilter(List<string> unacceptableDirectoryNames)
        {
            UnacceptableDirectoryNames = unacceptableDirectoryNames;
        }

        public DirectorySearchFilter()
        {
            UnacceptableDirectoryNames = new List<string>();
        }

        public bool IsAcceptable(FileDescriptor descriptor)
        {
            return descriptor.Directory.Split(Path.DirectorySeparatorChar)
                .Where(folder =>
                    UnacceptableDirectoryNames.Contains(folder) || !Regex.IsMatch(folder, RegularExpression))
                .ToList().Count == 0;
        }
    }
}