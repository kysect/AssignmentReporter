using System;
using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.Shared
{
    public record RepositoryCreationalInfoDto(
        string GithubToken,
        long RepositoryId,
        Guid StudentId,
        Guid SubjectGroupId,
        string WorkNumber,
        List<string> WhitelistedExtensions,
        List<string> BlacklistedDirectories,
        string Introduction,
        string Conclusion);
}