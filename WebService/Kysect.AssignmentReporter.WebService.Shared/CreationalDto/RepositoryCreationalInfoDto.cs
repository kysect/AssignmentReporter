using System;
using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.Shared.CreationalDto
{
    public class RepositoryCreationalInfoDto
    {
        public RepositoryCreationalInfoDto(
            string githubToken,
            long repositoryId,
            Guid studentId,
            Guid subjectGroupId,
            List<string> whitelistedExtensions,
            List<string> blacklistedDirectories,
            IReadOnlyList<SingleReportInfoDto> reports)
        {
            GithubToken = githubToken;
            RepositoryId = repositoryId;
            StudentId = studentId;
            SubjectGroupId = subjectGroupId;
            WhitelistedExtensions = whitelistedExtensions;
            BlacklistedDirectories = blacklistedDirectories;
            Reports = reports;
        }

        public string GithubToken { get; set; }
        public long RepositoryId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectGroupId { get; set; }
        public List<string> WhitelistedExtensions { get; set; }
        public List<string> BlacklistedDirectories { get; set; }
        public IReadOnlyList<SingleReportInfoDto> Reports { get; set; }
    }
}