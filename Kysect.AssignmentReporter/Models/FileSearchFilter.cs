using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class FileSearchFilter
    {
        public List<string> UnacceptableNames { get; set; }
        public List<string> UnacceptableExtensions { get; set; }

        public string RegularExpression { get; set; } = "";

        public FileSearchFilter(string regularExpression) : this()
        {
            RegularExpression = regularExpression;
        }

        /// <summary>
        /// </summary>
        /// <param name="unacceptableExtensions"> File extensions with period </param>
        /// <param name="unacceptableNames"></param>
        public FileSearchFilter(List<string> unacceptableExtensions, List<string> unacceptableNames)
        {
            UnacceptableNames = unacceptableNames;
            UnacceptableExtensions = unacceptableExtensions;
        }

        public FileSearchFilter()
        {
            UnacceptableNames = new List<string>();
            UnacceptableExtensions = new List<string>();
        }

        public bool IsAcceptable(FileDescriptor descriptor)
        {
            return !UnacceptableNames.Contains(descriptor.Name) &&
                   !UnacceptableExtensions.Contains(descriptor.Extension) &&
                   Regex.IsMatch(descriptor.NameWithExtension, RegularExpression);
        }
    }
}