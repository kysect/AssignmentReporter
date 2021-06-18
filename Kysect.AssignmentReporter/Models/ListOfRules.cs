﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models
{
    public abstract class ListOfRules
    {
        public List<string> FileNames { get; set; }
        public List<string> FileFormats { get; set; }
        public List<string> Directories { get; set; }

        public ListOfRules(List<string> fileNames, List<string> fileFormats, List<string> directories)
        {
            FileNames = fileNames;
            FileFormats = fileFormats;
            Directories = directories;
        }

        public ListOfRules() { }
        public bool FileIsAcceptable(string fileName)
        {
            return FileNames == null || (FileNames?.Contains(fileName) ?? true);
        }
        public bool FormatIsAcceptable(string fileFormat)
        {
            return FileFormats == null || (FileFormats?.Contains(fileFormat) ?? true);
        }
        public bool FileIsNotAcceptable(string fileName)
        {
            return FileNames == null || (!FileNames?.Contains(fileName) ?? true);
        }
        public bool FormatIsNotAcceptable(string fileFormat)
        {
            return FileFormats == null || (FileFormats?.Contains(fileFormat) ?? true);
        }
    }
}
