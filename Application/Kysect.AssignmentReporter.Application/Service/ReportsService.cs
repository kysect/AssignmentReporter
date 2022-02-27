using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kysect.AssignmentReporter.Application.Abstractions;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.OfficeIntegration;
using Kysect.AssignmentReporter.SourceCodeProvider;
using Kysect.AssignmentReporter.WebService.DAL.Entities;
using Kysect.AssignmentReporter.WebService.Server.Repository;
using Kysect.AssignmentReporter.WebService.Shared;
using Kysect.AssignmentReporter.WebService.Shared.CreationalDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kysect.AssignmentReporter.WebService.Server.Service
{
    public class ReportsService
    {
        private readonly IAssignmentReporterContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ReportsService(IAssignmentReporterContext context, IConfiguration configuration, IRepository repository, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        public void CreateReports(RepositoryCreationalInfoDto infoDto)
        {
            var provider = new OctoitSourceCodeProvider(infoDto.GithubToken);
            IReadOnlyList<GithubRepositoryInfo> githubRepositoryInfos = provider.GetRepositories().Where(repositoryInfo => infoDto.RepositoryId == repositoryInfo.Id).ToList();
            if (githubRepositoryInfos.Count() == 0)
            {
                throw new InvalidOperationException("Repository not found");
            }

            provider.Repository = githubRepositoryInfos.First();

            var template = _configuration.GetSection("TemplatePath").Value;
            if (template is null)
            {
                throw new InvalidOperationException("Template path not found, check server configuration");
            }

            SubjectGroup subjectGroup = _context.SubjectGroups
                                             .Include(x => x.Teacher)
                                             .Include(x => x.Subject)
                                             .FirstOrDefault(x => x.Id == infoDto.SubjectGroupId)
                                         ?? throw new InvalidOperationException("Subject group not found");

            Student student = _context.Students
                                   .Include(x => x.Group)
                                   .FirstOrDefault(x => x.Id == infoDto.StudentId)
                               ?? throw new InvalidOperationException("Student not found");

            if (!subjectGroup.Students.Contains(student))
            {
                throw new InvalidOperationException("Student not found in subject group");
            }

            foreach (SingleReportInfoDto singleReportInfoDto in infoDto.Reports)
            {
                var coverPage = new CoverPageInfo(
                    subjectGroup.Teacher.FullName,
                    student.Group.Name,
                    student.FullName,
                    subjectGroup.Subject.Name,
                    singleReportInfoDto.WorkNumber,
                    template);

                var reportExtendedInfo = new ReportExtendedInfo(
                    singleReportInfoDto.Introduction,
                    singleReportInfoDto.Conclusion,
                    string.Empty);

                var files = new List<FileDescriptor>();

                foreach (var folder in singleReportInfoDto.Folders.Distinct())
                {
                    files.AddRange(provider.GetFiles(
                        new FileSearchFilter(new SearchSettings()
                        {
                            BlackDirectories = infoDto.BlacklistedDirectories.Select(x => new Regex(x)).ToList(),
                            WhiteFileFormats = infoDto.WhitelistedExtensions,
                        }),
                        folder));
                }

                var generator = new DocumentReportGenerator(coverPage);
                using (MemoryStream reportStream = generator.GenerateStream(files, reportExtendedInfo))
                {
                    reportStream.Position = 0;
                    FileEntry file = _repository.Save(reportStream).Result;
                    var report = new Report(
                        subjectGroup.Subject,
                        student,
                        subjectGroup.Teacher,
                        singleReportInfoDto.WorkNumber,
                        file,
                        $"{student.FullName}_{student.Group.Name}_{subjectGroup.Subject.Name}_{singleReportInfoDto.WorkNumber}.docx");
                    _context.Reports.Add(report);
                    _context.SaveChanges();
                }
            }
        }

        public IReadOnlyList<GithubRepositoryInfo> GetRepositories(string token)
        {
            var provider = new OctoitSourceCodeProvider(token);
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
                .ProjectTo<ReportDto>(_mapper.ConfigurationProvider)
                .ToList();
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