using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Models;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class GithubSourceCodeProvider : ISourceCodeProvider
    {
        private string _localStoragePath;
        private string _repositoryOwner;
        private string _repositoryName;
        private string _url;
        private GitUserData _data;

        public GithubSourceCodeProvider(string owner, string name, GitUserData data)
        {
            _repositoryOwner = owner;
            _repositoryName = name;
            _localStoragePath = $@"C:\Users\{Environment.UserName}\AppData\Local\AssignmentReporterTmp\{_repositoryOwner}\{_repositoryName}";
            _url = $"https://github.com/{_repositoryOwner}/{_repositoryName}.git";
            _data = data;
        }
        public List<FileDescriptor> GetFiles()
        {
            string path = $@"C:\Users\{Environment.UserName}\AppData\Local\AssignmentReporterTmp";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            GetRepository();
            dirInfo.CreateSubdirectory($"{ _repositoryOwner}\\{ _repositoryName}");
            return new FileSystemSourceCodeProvider(_localStoragePath).GetFiles();
        }

        public void GetRepository()
        {
            if (!Repository.IsValid(_localStoragePath))
            {
              Repository.Clone(_url, _localStoragePath);
            }
            else
            { 
                var repository = new Repository(_localStoragePath);
                PullOptions options = new PullOptions();
                options.FetchOptions = new FetchOptions();
                options.FetchOptions.CredentialsProvider = new CredentialsHandler(
                    (_url, usernameFromUrl, types) => new UsernamePasswordCredentials());
                var signature = new Signature(new Identity($"{_data.Username}", $"{_data.Email}"), DateTimeOffset.Now);
                Commands.Pull(repository, signature, options);
            }
        }
    }
}