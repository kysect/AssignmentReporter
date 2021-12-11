﻿using System.IO;
using System.Text.Json.Serialization;

namespace Kysect.AssignmentReporter.WebService.Shared
{
    public class FileDto
    {
        private FileDto()
        {
        }

        public FileDto(string name, Stream stream)
        {
            Name = name;
            Stream = new MemoryStream();
            long pos = stream.Position;
            stream.Position = 0;
            stream.CopyTo(Stream);
            Stream.Position = pos;
        }

        public string Name { get; private init; }

        public byte[] Data
        {
            get => Stream.ToArray();
            private init => Stream = new MemoryStream(value);
        }
        [JsonIgnore]
        public MemoryStream Stream { get; private init; }
    }
}