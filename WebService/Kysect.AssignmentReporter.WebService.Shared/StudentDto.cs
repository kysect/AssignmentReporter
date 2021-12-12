using System;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared
{
    public record StudentDto(Guid Id, string Name, MinimalGroupDto Group)
    {
        public static StudentDto FromStudent(Student student)
            => new StudentDto(student.Id, student.FullName, MinimalGroupDto.FromGroup(student.Group));
    }
}