using System;

namespace Kysect.AssignmentReporter.Models
{
    public class FileDescriptor
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public string Directory { get; set; }

        public FileDescriptor(string name, string directory)
        {
            Name = name;
            Directory = directory;
            Content = String.Empty;
        }

        public void AddLine(string text)
        {
            Content += $"\n{text}";
        }
    }
}