using System.IO;

namespace Kysect.AssignmentReporter.Models
{
    public class FileDescriptor
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string NameWithExtension => Name + Extension;
        public string Directory { get; set; }

        public FileDescriptor(string name, string directory)
        {
            Name = Path.GetFileNameWithoutExtension(name);
            Extension = Path.GetExtension(name);
            Directory = directory;
        }
    }
}