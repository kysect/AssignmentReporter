using System;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record MinimalTeacherDto(Guid Id, string FullName)
{
    public static MinimalTeacherDto FromTeacher(Teacher teacher)
    {
        return new MinimalTeacherDto(
            teacher.Id,
            teacher.FullName);
    }
}