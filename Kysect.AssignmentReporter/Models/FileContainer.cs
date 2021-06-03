namespace Kysect.AssignmentReporter.Models
{
    public class FileContainer : FileDescriptor
    {
        public FileContainer(string name, string directory, string content) : this(name, directory)
        {
            Content = content;
        }

        public FileContainer(string name, string directory) : base(name, directory) { }

        public FileContainer(FileDescriptor descriptor, string content)
            : this(descriptor.NameWithExtension, descriptor.Directory, content) { }

        public FileContainer(FileDescriptor descriptor)
            : this(descriptor.Name, descriptor.Directory) { }

        public string Content { get; set; }
    }
}