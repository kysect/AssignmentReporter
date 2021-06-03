﻿using System;
using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using LibGit2Sharp;
using Microsoft.Alm.Authentication;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class GithubSourceCodeProvider : ISourceCodeProvider
    {
        private readonly GitUserData _data;
        private string _localStoragePath;
        private readonly string _repositoryName;
        private readonly string _repositoryOwner;
        private readonly string _url;

        public GithubSourceCodeProvider(string owner, string name, string rootPath, GitUserData data)
        {
            _repositoryOwner = owner;
            _repositoryName = name;
            _localStoragePath = rootPath;
            _url = $"https://github.com/{_repositoryOwner}/{_repositoryName}.git";
            _data = data;
        }

        public List<FileContainer> GetFiles()
        {
            char separator = Path.DirectorySeparatorChar;
            EnsureParentDirectoryExist(_localStoragePath)
                .CreateSubdirectory($"{_repositoryOwner}{separator}{_repositoryName}");
            _localStoragePath += $"{_repositoryOwner}{separator}{_repositoryName}";
            DownloadRepositoryFromGit();
            return new FileSystemSourceCodeProvider(_localStoragePath).GetFiles();
        }

        public string DownloadRepositoryFromGit()
        {
            Credential CredentialsInfo =
                new BasicAuthentication(new SecretStore("git")).GetCredentials(new TargetUri("https://github.com"));

            if (!Repository.IsValid(_localStoragePath))
            {
                var options = new CloneOptions();
                options.CredentialsProvider = (_url, usernameFromUrl, types) => new UsernamePasswordCredentials
                {
                    Username = CredentialsInfo.Username,
                    Password = CredentialsInfo.Password
                };
                Repository.Clone(_url, _localStoragePath, options);
            }
            else
            {
                var repository = new Repository(_localStoragePath);
                var options = new PullOptions();

                options.FetchOptions = new FetchOptions();
                options
                    .FetchOptions
                    .CredentialsProvider = (_url, usernameFromUrl, types) => new UsernamePasswordCredentials
                {
                    Username = CredentialsInfo.Username,
                    Password = CredentialsInfo.Password
                };

                var signature = new Signature(
                    new Identity($"{CredentialsInfo.Username}", $"{_data.Email}"), DateTimeOffset.Now);

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