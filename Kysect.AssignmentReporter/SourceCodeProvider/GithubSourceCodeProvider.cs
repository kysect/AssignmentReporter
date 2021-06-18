using System;
using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using LibGit2Sharp;
using Microsoft.Alm.Authentication;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class GithubSourceCodeProvider : ISourceCodeProvider
    {
        private string _localStoragePath;
        private readonly string _repositoryOwner;
        private readonly string _repositoryName;
        private readonly string _url;
        private readonly GitUserData _data;
        private readonly FileSearchFilter _fileSearchFilter;

        public GithubSourceCodeProvider(string owner, string name, string rootPath, GitUserData data, FileSearchFilter fileSearchFilter)
        {
            _repositoryOwner = owner;
            _repositoryName = name;
            _localStoragePath = rootPath;
            _url = $"https://github.com/{_repositoryOwner}/{_repositoryName}.git";
            _data = data;
            _fileSearchFilter = fileSearchFilter;
        }
        
        public List<FileDescriptor> GetFiles()
        {
            char separator = Path.DirectorySeparatorChar;
            EnsureParentDirectoryExist(_localStoragePath)
                .CreateSubdirectory($"{_repositoryOwner}{separator}{_repositoryName}");
            _localStoragePath += $"{_repositoryOwner}{separator}{_repositoryName}";
            DownloadRepositoryFromGit();
            return new FileSystemSourceCodeProvider(_localStoragePath, _fileSearchFilter).GetFiles();
        }

        public string DownloadRepositoryFromGit()
        {
            Credential credentialsInfo = 
                new BasicAuthentication(new SecretStore("git")).GetCredentials(new TargetUri("https://github.com"));

            if (!Repository.IsValid(_localStoragePath))
            {
                var options = new CloneOptions
                {
                    CredentialsProvider = (_url, usernameFromUrl, types) => new UsernamePasswordCredentials
                    {
                        Username = credentialsInfo.Username, Password = credentialsInfo.Password
                    }
                };
                Repository.Clone(_url, _localStoragePath, options);
            }
            else
            {
                var repository = new Repository(_localStoragePath);
                var options = new PullOptions
                {
                    FetchOptions = new FetchOptions
                    {
                        CredentialsProvider = (_url, usernameFromUrl, types) => new UsernamePasswordCredentials
                        {
                            Username = credentialsInfo.Username, Password = credentialsInfo.Password
                        }
                    }
                };


                var signature = new Signature(
                    new Identity($"{credentialsInfo.Username}", $"{_data.Email}"), DateTimeOffset.Now);

                Commands.Pull(repository, signature, options);
            }

            return _localStoragePath;
        }

        public DirectoryInfo EnsureParentDirectoryExist(string _path)
        {
            var dirInfo = new DirectoryInfo(_path);
            if (!dirInfo.Exists) dirInfo.Create();
            return dirInfo;
        }
    }
}