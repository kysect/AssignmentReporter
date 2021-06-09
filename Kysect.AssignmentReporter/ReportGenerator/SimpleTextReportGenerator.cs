using System;
using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class SimpleTextReportGenerator : IReportGenerator
    {
        public string Extension => "txt";

        public FileDescriptor Generate(FileDescriptor result, List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            throw new NotImplementedException();
        }
    }
}