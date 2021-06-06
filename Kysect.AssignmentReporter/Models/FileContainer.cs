using System.IO;
using System.Reflection;

namespace Kysect.AssignmentReporter.Models
{
    public class FileContainer : FileDescriptor
    {
        public FileContainer(FileInfo info) : base(info)
        {
            Content = File.ReadAllText(info.FullName);
        }

        public FileContainer(string name, string extension, string direction)
            : base(name, extension, direction) { }

        public FileContainer(string name, string extension, string direction, string content)
            : this(name, extension, direction)
        {
            Content = content;
        }
        
        public FileContainer(FileDescriptor descriptor)
            : this(descriptor.Name, descriptor.Extension, descriptor.Directory) {}

        public FileContainer(FileDescriptor descriptor, string content)
            : this(descriptor)
        {
            Content = content;
        }
        
        public string Content { get; set; }
    }
}