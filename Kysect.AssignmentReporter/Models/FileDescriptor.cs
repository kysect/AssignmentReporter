namespace Kysect.AssignmentReporter.Models
{
    public class FileDescriptor
    {
        public string Name { get; }
        public string Content { get; }
        public string RootDirectory { get; }

        public FileDescriptor(string name, string content, string rootDirectory)
        {
            Name = name;
            RootDirectory = rootDirectory;
            Content = content;
        }
    }
}