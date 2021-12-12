using System;
using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.Shared
{
    public record RepositoryCreationalInfoDto(
        string GithubToken,
        long RepositoryId,
        Guid StudentId,
        Guid SubjectGroupId,
        List<string> WhitelistedExtensions,
        List<string> BlacklistedDirectories,
        IReadOnlyList<SingleReportInfoDto> Reports);
}