using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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
        public DbSet<FileEntry> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasMany(x => x.Students).WithOne(x => x.Group);
            modelBuilder.Entity<Teacher>().HasMany(x => x.SubjectGroups).WithOne(x => x.Teacher);
            modelBuilder.Entity<SubjectGroup>().HasMany(x => x.Students);
            modelBuilder.Entity<SubjectGroup>().HasOne(x => x.Subject);
            modelBuilder.Entity<Report>().HasOne(x => x.Subject);
            modelBuilder.Entity<Report>().HasOne( x => x.Student);
            modelBuilder.Entity<Report>().HasOne(x => x.Teacher);
            modelBuilder.Entity<Report>().HasOne(x => x.File);
            modelBuilder.Entity<Subject>().HasKey(x => x.Name);
            base.OnModelCreating(modelBuilder);
        }
    }
}