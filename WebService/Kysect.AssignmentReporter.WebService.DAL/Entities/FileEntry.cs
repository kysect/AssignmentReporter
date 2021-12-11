using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class FileEntry
    {
        public FileEntry()
        {
        }

        public FileEntry(string fileName)
        {
            FileExtension = Path.GetExtension(fileName);
            FileName = Path.GetFileNameWithoutExtension(fileName);
        }

        public Guid Id { get; private init; }
        public string FileName { get; private init; }
        public string FileExtension { get; private init; }

        [NotMapped]
        public string FullName => $"{FileName}{FileExtension}";
    }
}