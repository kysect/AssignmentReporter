using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record SubjectGroupDto(Guid Id, IReadOnlyList<StudentDto> Students, Guid TeacherId, SubjectDto Subject)
{
    public static SubjectGroupDto FromSubjectGroup(SubjectGroup subjectGroup)
    {
        return new SubjectGroupDto(
            subjectGroup.Id,
            subjectGroup.Students.Select(x => StudentDto.FromStudent(x)).ToList(),
            subjectGroup.Teacher.Id,
            SubjectDto.FromSubject(subjectGroup.Subject));
    }
}