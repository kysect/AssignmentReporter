using System;

namespace Kysect.AssignmentReporter.Models
{
    public class FileSearchFilter
    {
        public bool IsAcceptable(string fileName, string[] formats)
        {
            foreach (var format in formats)
            {
                if (fileName.EndsWith(format))
                {
                    return true;
                }
            }
            return false;
        }
    }
}