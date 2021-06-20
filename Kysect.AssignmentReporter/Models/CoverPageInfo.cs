using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models
{
   public class CoverPageInfo
   { 
        public string GroupNumber { get; set; }
        public string WorkNumber { get; set; } 
        public string Discipline { get; set; } 
        public string FullName { get; set; } 
        public string TeacherName { get; set; }
        public string PathToTitlePage { get; set; }
        public CoverPageInfo(string teacherName, string groupNumber, string fullName, string discipline, string workNumber, string pathToTitlePage)
        {
            TeacherName = teacherName;
            FullName = fullName;
            Discipline = discipline;
            WorkNumber = workNumber;
            PathToTitlePage = pathToTitlePage;
            GroupNumber = groupNumber;
        }
   }
}
