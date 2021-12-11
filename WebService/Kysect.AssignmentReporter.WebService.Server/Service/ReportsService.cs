using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;
using Kysect.AssignmentReporter.WebService.DAL.Database;
using Kysect.AssignmentReporter.WebService.DAL.Entities;
using Kysect.AssignmentReporter.WebService.Server.Repository;
using Kysect.AssignmentReporter.WebService.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Kysect.AssignmentReporter.WebService.Server.Service
{
    public class ReportsService
    {
        private AssignmentReporterContext _context;
        private IConfiguration _configuration;
        private IRepository _repository;

        public ReportsService(AssignmentReporterContext context, IConfiguration configuration, IRepository repository)
        {
            _context = context;
            _configuration = configuration;
            _repository = repository;
        }

        public void CreateSingleReport(RepositoryCreationalInfoDto infoDto)
        {
            var provider = new GithubSourceCodeProvider(infoDto.GithubToken);
            IReadOnlyList<GithubRepositoryInfo> githubRepositoryInfos = provider.GetRepositories().Where(repositoryInfo => infoDto.RepositoryId == repositoryInfo.Id).ToList();
            if (githubRepositoryInfos.Count() == 0)
            {
                throw new InvalidOperationException("Repository not found");
            }

            provider.Repository = githubRepositoryInfos.First();

            IReadOnlyList<FileDescriptor> files = provider.GetFiles(new SearchSettings()
            {
                BlackDirectories = infoDto.BlacklistedDirectories.Select(x => new Regex(x)).ToList(),
                WhiteFileFormats = infoDto.WhitelistedExtensions
            });

            var template = _configuration.GetSection("TemplatePath").Value;
            if (template is null)
            {
                throw new InvalidOperationException("Template path not found, check server configuration");
            }

            SubjectGroup? subjectGroup = _context.SubjectGroups
                                             .Include(x => x.Teacher)
                                             .Include(x => x.Subject)
                                             .FirstOrDefault(x => x.Id == infoDto.SubjectGroupId)
                                         ?? throw new InvalidOperationException("Subject group not found");

            Student? student = _context.Students
                                   .Include(x => x.Group)
                                   .FirstOrDefault( x => x.Id == infoDto.StudentId)
                               ?? throw new InvalidOperationException("Student not found");

            if(!subjectGroup.Students.Contains(student))
            {
                throw new InvalidOperationException("Student not found in subject group");
            }

            var coverPage = new CoverPageInfo(
                subjectGroup.Teacher.FullName,
                student.Group.Name,
                student.FullName,
                subjectGroup.Subject.Name,
                infoDto.WorkNumber,
                template);

            var reportExtendedInfo = new ReportExtendedInfo(
                infoDto.Introduction,
                infoDto.Conclusion,
                String.Empty);

            var generator = new DocumentReportGenerator(coverPage);
            MemoryStream? reportStream = generator.GenerateStream(files, reportExtendedInfo);
            FileEntry file = _repository.Save(reportStream).Result;
            var report = new Report(
                subjectGroup.Subject,
                student,
                subjectGroup.Teacher,
                infoDto.WorkNumber,
                file,
                $"{student.FullName}_{student.Group.Name},_{subjectGroup.Subject.Name}_{infoDto.WorkNumber}.docx");
            _context.Reports.Add(report);
            _context.SaveChanges();
        }

        public IReadOnlyList<GithubRepositoryInfo> GetRepositories(string token)
        {
            var provider = new GithubSourceCodeProvider(token);
            return provider.GetRepositories();
        }

        public IReadOnlyList<ReportDto> GetReports()
        {
            return _context.Reports
                .Include(x => x.Student)
                .ThenInclude(x => x.Group)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .ThenInclude(x => x.SubjectGroups)
                .Select(x => ReportDto.FromReport(x)).ToList();
        }

        public FileDto DownloadReport(Guid id)
        {

            Report report = _context.Reports
                                .Include(x => x.File)
                                .FirstOrDefault(x => x.Id == id)
                            ?? throw new InvalidOperationException("Report not found");

            return _repository.Get(report).Result;
        }

        public void Delete(Guid id)
        {
            Report report = _context.Reports.Find(id)
                            ?? throw new InvalidOperationException("Report not found");

            _repository.Delete(report);
            _context.Reports.Remove(report);
            _context.SaveChanges();
        }
    }
}