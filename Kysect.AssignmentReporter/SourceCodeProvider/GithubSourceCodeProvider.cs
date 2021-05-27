﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Alm.Authentication;
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

        public GithubSourceCodeProvider(string owner, string name,string rootPath, GitUserData data)
        {
            _repositoryOwner = owner;
            _repositoryName = name;
            _localStoragePath = rootPath;
            _url = $"https://github.com/{_repositoryOwner}/{_repositoryName}.git";
            _data = data;
        }
        public List<FileDescriptor> GetFiles()
        {
            EnsureParentDirectoryExist(_localStoragePath).CreateSubdirectory($"{ _repositoryOwner}\\{ _repositoryName}");
            _localStoragePath +=$@"\{_repositoryOwner}\{_repositoryName}";
            DownloadRepositoryFromGit();
            return new FileSystemSourceCodeProvider(_localStoragePath).GetFiles();
        }

        public string DownloadRepositoryFromGit()
        {
            var CredentialsInfo = new BasicAuthentication(new SecretStore("git")).GetCredentials(new TargetUri("https://github.com"));

            if (!Repository.IsValid(_localStoragePath))
            {
                CloneOptions options = new CloneOptions();
                options.CredentialsProvider = (_url, usernameFromUrl, types) => new UsernamePasswordCredentials()
                {
                    Username = CredentialsInfo.Username,
                    Password = CredentialsInfo.Password
                };
                Repository.Clone(_url, _localStoragePath, options);
            }
            else
            { 
                var repository = new Repository(_localStoragePath);
                PullOptions options = new PullOptions();

                options.FetchOptions = new FetchOptions();
                options
                    .FetchOptions
                    .CredentialsProvider = new CredentialsHandler(
                    (_url, usernameFromUrl, types) => new UsernamePasswordCredentials()
                    {
                        Username = CredentialsInfo.Username,
                        Password = CredentialsInfo.Password
                    });

                var signature = new Signature(
                    new Identity($"{_data.Username}", $"{_data.Email}"), DateTimeOffset.Now);

                Commands.Pull(repository, signature, options);
            }

            return _localStoragePath;
        }
        public DirectoryInfo EnsureParentDirectoryExist(string _path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            return dirInfo;
        }
    }
}