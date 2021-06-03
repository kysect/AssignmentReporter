namespace Kysect.AssignmentReporter.Models
{
    public class FileContainer : FileDescriptor
    {
        public FileContainer(string name, string directory, string content) : base(name, directory)
        {
            Content = content;
        }

        public FileContainer(FileDescriptor descriptor, string content)
            : this(descriptor.NameWithExtension, descriptor.Directory, content) { }

        public string Content { get; set; }
    }
}