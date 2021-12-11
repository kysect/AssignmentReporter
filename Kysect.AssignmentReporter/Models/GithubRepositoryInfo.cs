using System;
using Octokit;

namespace Kysect.AssignmentReporter.Models
{
    public class GithubRepositoryInfo
    {
        public GithubRepositoryInfo(Repository repository)
        {
            Id = repository.Id;
            FullName = repository.Name;
            Name = repository.Name;
            Url = new Uri(repository.Url);
        }

        public long Id { get; }
        public string Name { get; }
        public string FullName { get; }
        public Uri Url { get; }
    }
}