﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Models;
using LibGit2Sharp;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class GithubSourceCodeProvider : ISourceCodeProvider
    {
        private string _localStoragePath;
        private string _repositoryOwner;
        private string _repositoryName;

        public GithubSourceCodeProvider(string owner, string name)
        {
            _repositoryOwner = owner;
            _repositoryName = name;
            _localStoragePath = $@"C:\Users\{System.Environment.UserName}\AppData\Local\AssignmentReporterTmp\{_repositoryOwner}\{_repositoryName}";
        }
        public List<FileDescriptor> GetFiles()
        {
            string path = $@"C:\Users\{System.Environment.UserName}\AppData\Local\AssignmentReporterTmp";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            dirInfo.CreateSubdirectory($"{ _repositoryOwner}\\{ _repositoryName}");
            if (!Repository.IsValid(_localStoragePath))
            {
                Repository.Clone($"https://github.com/{_repositoryOwner}/{_repositoryName}.git", _localStoragePath);
            }

            return new FileSystemSourceCodeProvider(_localStoragePath).GetFiles();//считывание файлов через другой класс. Надеюсь так можно.
        }
    }
}