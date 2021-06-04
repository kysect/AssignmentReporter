using System.IO;
using System.Linq;

namespace Kysect.AssignmentReporter.Models
{
    public class FileDescriptor
    {
        public FileDescriptor(FileInfo info)
        {
            Name = Path.GetFileNameWithoutExtension(info.Name);
            Extension = info.Extension.Replace(".", "");
            Directory = info.DirectoryName;
            FileInfo = info;
        }

        public FileDescriptor(string name, string extension, string directory)
        {
            Name = name;
            Extension = extension;
            Directory = directory;
            FileInfo = new FileInfo(Path.Combine(directory, NameWithExtension));
        }
        
        
        public string Name { get; }
        public string Extension { get; }
        public string NameWithExtension => Name + '.' + Extension;
        public string Directory { get; }
        public FileInfo FileInfo { get; }
    }
}