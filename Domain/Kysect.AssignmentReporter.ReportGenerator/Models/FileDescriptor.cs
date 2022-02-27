using System;
using System.IO;
using System.Text;

namespace Kysect.AssignmentReporter.Models
{
    public class FileDescriptor : IDisposable
    {
        public FileDescriptor(string name, Stream stream, string rootDirectory)
        {
            Name = name;
            Stream = new MemoryStream();
            long position = stream.Position;
            stream.Position = 0;
            stream.CopyTo(Stream);
            stream.Position = position;
            RootDirectory = rootDirectory;
        }

        public FileDescriptor(string name, string content, string rootDirectory)
        {
            Name = name;
            Stream = new MemoryStream();
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            Stream.Write(bytes, 0, bytes.Length);
            RootDirectory = rootDirectory;
        }

        public string Name { get; }
        public MemoryStream Stream { get; }
        public string Content => Encoding.UTF8.GetString(Stream.ToArray());
        public string RootDirectory { get; }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}