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
        private GitHubClient _client;
        private ICredentialStore _credentialStore;
        private string _token;
        public GithubSourceCodeProvider(string token, string username)
        {
            Repository = null;
            _credentialStore = new InMemoryCredentialStore(new Credentials(token));
            _client = new GitHubClient(new ProductHeaderValue("AssignmentReporter"), _credentialStore);
        }
        public IReadOnlyList<FileDescriptor> GetFiles()
        {
            if (Repository is null)
                throw new InvalidOperationException("You must specify repository first");
            var descriptors = new List<FileDescriptor>();
            GetFolderFiles(descriptors, new SearchSettings());
            return descriptors;
        }
        
        public IReadOnlyList<FileDescriptor> GetFiles(SearchSettings settings, string folder = "/")
        {
            if (Repository is null)
                throw new InvalidOperationException("You must specify repository first");
            var descriptors = new List<FileDescriptor>();
            GetFolderFiles(descriptors, settings, folder);
            return descriptors;
        }
        
        private void GetFolderFiles(List<FileDescriptor> files, SearchSettings settings, string Path = "/")
        { 
            var content = _client.Repository.Content.GetAllContents(Repository.Id, Path).Result;

            foreach (var repositoryContent in content.Where(c => (c.Name[0] != '.')))
            {
                if (repositoryContent.Type == ContentType.Dir 
                    && settings.DirectoryIsAcceptable(repositoryContent.Name))
                    GetFolderFiles(files, settings, repositoryContent.Path);
                else if (repositoryContent.Type == ContentType.File 
                         && settings.FileIsAcceptable(repositoryContent.Name) 
                         && settings.FormatIsAcceptable(System.IO.Path.GetExtension(repositoryContent.Name)))
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
        
        public GithubRepositoryInfo Repository { get; set; }
        
        public IReadOnlyList<GithubRepositoryInfo> GetRepositories()
        {
            var repos = _client.Repository.GetAllForCurrent().Result;
            return repos.Select(r => new GithubRepositoryInfo(r)).ToList();
        }
    }
}