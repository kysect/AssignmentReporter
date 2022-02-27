using Kysect.AssignmentReporter.Domain;
using Kysect.AssignmentReporter.Dto;

namespace Kysect.AssignmentReporter.Application;

public interface IRepository
{
    Task<FileEntry> Save(Stream stream);
    Task<FileDto> Get(Report report);
    Task Delete(Report report);
}