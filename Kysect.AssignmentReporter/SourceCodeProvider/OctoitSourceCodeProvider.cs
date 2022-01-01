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
    public class OctoitSourceCodeProvider : ISourceCodeProvider
    {
        private readonly GitHubClient _client;

        public OctoitSourceCodeProvider(string token)
        {
            Repository = null;
            ICredentialStore credentialStore = new InMemoryCredentialStore(new Credentials(token));
            _client = new GitHubClient(new ProductHeaderValue("AssignmentReporter"), credentialStore);
        }

        public GithubRepositoryInfo Repository { get; set; }

        public IReadOnlyList<FileDescriptor> GetFiles(FileSearchFilter fileSearchFilter)
        {
            if (Repository is null)
            {
                throw new InvalidOperationException("You must specify repository first");
            }

            var descriptors = new List<FileDescriptor>();
            GetFolderFiles(descriptors, fileSearchFilter);
            return descriptors;
        }

        public IReadOnlyList<FileDescriptor> GetFilesInternal(FileSearchFilter fileSearchFilter, string folder = "/")
        {
            if (Repository is null)
            {
                throw new InvalidOperationException("You must specify repository first");
            }

            var descriptors = new List<FileDescriptor>();
            GetFolderFiles(descriptors, fileSearchFilter, folder);
            return descriptors;
        }

        public IReadOnlyList<GithubRepositoryInfo> GetRepositories()
        {
            IReadOnlyList<Repository> repos = _client.Repository.GetAllForCurrent().Result;
            return repos.Select(r => new GithubRepositoryInfo(r)).ToList();
        }

        private void GetFolderFiles(List<FileDescriptor> files, FileSearchFilter fileSearchFilter, string path = "/")
        {
            IReadOnlyList<RepositoryContent> content = _client.Repository.Content.GetAllContents(Repository.Id, path).Result;

            foreach (RepositoryContent repositoryContent in content.Where(c => (c.Name[0] != '.')))
            {
                if (repositoryContent.Type == ContentType.Dir && fileSearchFilter.SearchSettings.DirectoryIsAcceptable(repositoryContent.Name))
                {
                    GetFolderFiles(files, fileSearchFilter, repositoryContent.Path);
                }
                else if (
                    repositoryContent.Type == ContentType.File &&
                    fileSearchFilter.SearchSettings.FileIsAcceptable(repositoryContent.Name) &&
                    fileSearchFilter.SearchSettings.FormatIsAcceptable(Path.GetExtension(repositoryContent.Name)))
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