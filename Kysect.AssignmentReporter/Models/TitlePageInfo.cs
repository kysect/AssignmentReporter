using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models
{
   public class TitlePageInfo
   {
       public string GroupNumber { get; set; } = string.Empty;
        public string WorkNumber { get; set; } = string.Empty;
        public string Discipline { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string PathToTitlePage { get; set; }
        public TitlePageInfo(string teacherName, string groupNumber, string fullName, string discipline, string workNumber, string pathToTitlePage)
        {
            TeacherName = teacherName;
            FullName = fullName;
            Discipline = discipline;
            WorkNumber = workNumber;
            PathToTitlePage = pathToTitlePage;
            GroupNumber = groupNumber;
        }
        public TitlePageInfo(string pathToTitlePage)
        {
            PathToTitlePage = pathToTitlePage;
          
        }
    }
}
