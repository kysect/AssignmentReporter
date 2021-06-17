namespace Kysect.AssignmentReporter.Models
{
    public class FileDescriptor
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string RootDirectory { get; set; }

        public FileDescriptor(string name, string content, string rootDirectory)
        {
            Name = name;
            RootDirectory = rootDirectory;
            Content = content;
        }
    }
}