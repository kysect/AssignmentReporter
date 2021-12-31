using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Octokit;
using Octokit.Internal;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class GithubSourceCodeProvider : ISourceCodeProvider
    {
        private readonly GitHubClient _client;

        public GithubSourceCodeProvider(string token)
        {
            Repository = null;
            ICredentialStore credentialStore = new InMemoryCredentialStore(new Credentials(token));
            _client = new GitHubClient(new ProductHeaderValue("AssignmentReporter"), credentialStore);
        }

        public GithubRepositoryInfo Repository { get; set; }

        public IReadOnlyList<FileDescriptor> GetFiles()
        {
            if (Repository is null)
            {
                throw new InvalidOperationException("You must specify repository first");
            }

            var descriptors = new List<FileDescriptor>();
            GetFolderFiles(descriptors, new SearchSettings());
            return descriptors;
        }

        public IReadOnlyList<FileDescriptor> GetFiles(SearchSettings settings, string folder = "/")
        {
            if (Repository is null)
            {
                throw new InvalidOperationException("You must specify repository first");
            }

            var descriptors = new List<FileDescriptor>();
            GetFolderFiles(descriptors, settings, folder);
            return descriptors;
        }

        public IReadOnlyList<GithubRepositoryInfo> GetRepositories()
        {
            IReadOnlyList<Repository> repos = _client.Repository.GetAllForCurrent().Result;
            return repos.Select(r => new GithubRepositoryInfo(r)).ToList();
        }

        private void GetFolderFiles(List<FileDescriptor> files, SearchSettings settings, string path = "/")
        {
            IReadOnlyList<RepositoryContent> content = _client.Repository.Content.GetAllContents(Repository.Id, path).Result;

            foreach (RepositoryContent repositoryContent in content.Where(c => (c.Name[0] != '.')))
            {
                if (repositoryContent.Type == ContentType.Dir && settings.DirectoryIsAcceptable(repositoryContent.Name))
                {
                    GetFolderFiles(files, settings, repositoryContent.Path);
                }
                else if (
                    repositoryContent.Type == ContentType.File &&
                    settings.FileIsAcceptable(repositoryContent.Name) &&
                    settings.FormatIsAcceptable(Path.GetExtension(repositoryContent.Name)))
                {
                    using (var client = new WebClient())
                    {
                        var download = client.DownloadData(repositoryContent.DownloadUrl);
                        using (var stream = new MemoryStream(download))
                        {
                            files.Add(new FileDescriptor(repositoryContent.Name, stream, repositoryContent.Path));
                        }
                    }
                }
            }
        }
    }
}