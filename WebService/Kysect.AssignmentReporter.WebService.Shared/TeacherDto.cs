using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record TeacherDto(Guid Id, string FullName, IReadOnlyList<Guid> SubjectGroupIds)
{
    public static TeacherDto FromTeacher(Teacher teacher)
    {
        return new TeacherDto(
            teacher.Id,
            teacher.FullName,
            teacher.SubjectGroups.Select(x => x.Id).ToList());
    }
}