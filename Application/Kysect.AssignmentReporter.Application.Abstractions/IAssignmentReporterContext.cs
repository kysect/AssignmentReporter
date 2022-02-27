using Kysect.AssignmentReporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kysect.AssignmentReporter.Application.Abstractions;

public interface IAssignmentReporterContext
{
    DbSet<Student> Students { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<Subject> Subjects { get; set; }
    DbSet<SubjectGroup> SubjectGroups { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<Report> Reports { get; set; }
    DbSet<FileEntry> Files { get; set; }

    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
}