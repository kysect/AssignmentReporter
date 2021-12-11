using System;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record ReportDto(
    Guid Id,
    SubjectDto Subject,
    StudentDto Student,
    TeacherDto Teacher,
    string WorkNumber)
{
    public static ReportDto FromReport(Report report)
    {
        return new ReportDto(
            report.Id,
            SubjectDto.FromSubject(report.Subject),
            StudentDto.FromStudent(report.Student),
            TeacherDto.FromTeacher(report.Teacher),
            report.WorkNumber);
    }
}