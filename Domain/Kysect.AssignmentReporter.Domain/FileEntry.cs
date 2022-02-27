using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Kysect.AssignmentReporter.Domain;

public class FileEntry
{
    public FileEntry(string fileName)
    {
        FileExtension = Path.GetExtension(fileName);
        FileName = Path.GetFileNameWithoutExtension(fileName);
    }

 #pragma warning disable CS8618
    private FileEntry()
    {
    }
 #pragma warning restore CS8618

    public Guid Id { get; private init; }
    public string FileName { get; private init; }
    public string FileExtension { get; private init; }

    [NotMapped]
    public string FullName => $"{FileName}{FileExtension}";
}