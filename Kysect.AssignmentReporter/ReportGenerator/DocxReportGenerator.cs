﻿using System;
using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class DocxReportGenerator : IReportGenerator
    {
        public string Extension => "docx";

        public FileContainer Generate(FileDescriptor result, List<FileContainer> files,
            DirectorySearchFilter directorySearchFilter,
            FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo)
        {
            throw new NotImplementedException();
        }
    }
}