using Kysect.AssignmentReporter.WebService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.AssignmentReporter.WebService.DAL.Database
{
    public sealed class AssignmentReporterContext : DbContext
    {
        public AssignmentReporterContext(DbContextOptions<AssignmentReporterContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectGroup> SubjectGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasMany<Student>().WithOne();
            modelBuilder.Entity<Teacher>().HasMany<SubjectGroup>().WithOne();
            modelBuilder.Entity<SubjectGroup>().HasOne<Group>();
            modelBuilder.Entity<SubjectGroup>().HasOne<Subject>();
            modelBuilder.Entity<Teacher>().HasMany<SubjectGroup>();
            modelBuilder.Entity<Report>().HasOne<Subject>();
            modelBuilder.Entity<Report>().HasOne<Student>();
            modelBuilder.Entity<Report>().HasOne<Teacher>();
            base.OnModelCreating(modelBuilder);
        }
    }
}