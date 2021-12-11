using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared
{
    public record GroupDto(string Name, List<StudentDto> Students)
    {
        public static GroupDto FromGroup(Group group)
        {
            return new GroupDto(group.Name, group.Students.Select(x => StudentDto.FromStudent(x)).ToList());
        }
    }
}