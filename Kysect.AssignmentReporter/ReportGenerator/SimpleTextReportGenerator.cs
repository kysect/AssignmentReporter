﻿using System;
using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class SimpleTextReportGenerator : IReportGenerator
    {
        public FileContainer Generate(FileDescriptor result, List<FileContainer> files,
            DirectorySearchFilter directorySearchFilter,
            FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo)
        {
            throw new NotImplementedException();
        }
    }
}