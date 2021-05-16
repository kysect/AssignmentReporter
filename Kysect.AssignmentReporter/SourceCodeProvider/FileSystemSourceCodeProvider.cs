using System;
using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using System.Linq;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class FileSystemSourceCodeProvider : ISourceCodeProvider
    {
        private string _rootDirectoryPath;

        public FileSystemSourceCodeProvider(string rootDirectoryPath)
        {
            _rootDirectoryPath = rootDirectoryPath;
        }
        public List<FileDescriptor> GetFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_rootDirectoryPath);
            var folders = GetDirectoryInfos(directoryInfo);
            return getFileDescriptors(folders);
        }

        private List<FileDescriptor> getFileDescriptors(List<DirectoryInfo> infos)
        {
            var files = new List<FileDescriptor>();
            foreach (var folder in infos)
            {
                foreach (var file in folder.GetFiles())
                {
                    if (file.Name.EndsWith(".cs"))
                    {
                        var newFile = new FileDescriptor(file.Name, file.Directory.Name);
                        using (StreamReader sr = new StreamReader(file.FullName))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                newFile.AddLine(line);
                            }
                        }
                        files.Add(newFile);
                    }
                }
            }
            return files;
        }
        public List<DirectoryInfo> GetDirectoryInfos(DirectoryInfo info)
        {
           var folders = new List<DirectoryInfo>();
           if (info.GetDirectories() != null)
           {
               foreach (var folder in info.GetDirectories())
               {
                   folders.AddRange(GetDirectoryInfos(folder));
               }
           }
           folders.AddRange(info.GetDirectories());
           return folders;
        }
    }
}