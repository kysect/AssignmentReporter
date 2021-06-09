using System;
using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class PdfReportGenerator : IReportGenerator
    {
        public string Extension => "pdf";

        public FileDescriptor Generate(FileDescriptor result, List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            throw new NotImplementedException();
        }
    }
}