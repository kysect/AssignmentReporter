using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class FileSearchFilter
    {
        public FileMask BlackList { get; set; }
        public FileMask Exceptions { get; set; }

        public FileSearchFilter(FileMask blackList, FileMask exceptions)
        {
            BlackList = blackList;
            Exceptions = exceptions;
        }

        public FileSearchFilter()
        {
            Exceptions = new FileMask();
            BlackList = new FileMask();
        }

        private bool NameIsAcceptable(FileDescriptor descriptor)
            => !BlackList.NameIntersection(descriptor.Name) || Exceptions.NameIntersection(descriptor.Name);

        private bool ExtensionsIsAcceptable(FileDescriptor descriptor)
            => !BlackList.ExtensionIntersection(descriptor.Extension) || Exceptions.ExtensionIntersection(descriptor.Extension);

        private bool DirectoryIsAcceptable(FileDescriptor descriptor)
            => !BlackList.DirectoryIntersection(descriptor.Directory) || Exceptions.DirectoryIntersection(descriptor.Directory);

        public bool FileIsAcceptable(FileDescriptor descriptor)
            => NameIsAcceptable(descriptor) &&
               ExtensionsIsAcceptable(descriptor) &&
               DirectoryIsAcceptable(descriptor);
    }
}