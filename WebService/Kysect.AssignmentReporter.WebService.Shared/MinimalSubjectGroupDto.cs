using System;
using System.Linq;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record MinimalSubjectGroupDto(Guid Id, MinimalTeacherDto MinimalTeacherDto, SubjectDto Subject)
{
    public static MinimalSubjectGroupDto FromSubjectGroup(SubjectGroup subjectGroup)
    {
        return new MinimalSubjectGroupDto(
            subjectGroup.Id,
            MinimalTeacherDto.FromTeacher(subjectGroup.Teacher),
            SubjectDto.FromSubject(subjectGroup.Subject));
    }
}