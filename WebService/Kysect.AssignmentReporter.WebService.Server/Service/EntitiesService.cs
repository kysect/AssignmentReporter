using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.WebService.DAL.Database;
using Kysect.AssignmentReporter.WebService.DAL.Entities;
using Kysect.AssignmentReporter.WebService.Shared;
using Microsoft.EntityFrameworkCore;
using Group = Kysect.AssignmentReporter.WebService.DAL.Entities.Group;

namespace Kysect.AssignmentReporter.WebService.Server.Service
{
    public class EntitiesService
    {
        private readonly AssignmentReporterContext _context;

        public EntitiesService(AssignmentReporterContext context)
        {
            _context = context;
        }

        public MinimalGroupDto AddGroup(MinimalGroupDto groupDto)
        {
            if (_context.Groups.Any(g => g.Name == groupDto.Name))
            {
                throw new ArgumentException("Group already exists");
            }

            var group = new Group(groupDto.Name);
            _context.Groups.Add(new Group(groupDto.Name));
            _context.SaveChanges();
            return MinimalGroupDto.FromGroup(group);
        }

        public void DeleteGroup(string groupName)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                          ?? throw new InvalidOperationException("No such group registered");

            if (group.Students.Count > 0)
            {
                throw new InvalidOperationException("Group is not empty");
            }

            _context.Groups.Remove(group);
            _context.SaveChanges();
        }

        public IReadOnlyList<MinimalGroupDto> GetGroups()
        {
            return _context.Groups
                .Select(x => MinimalGroupDto.FromGroup(x))
                .ToList();
        }

        public GroupDto GetGroup(string groupName)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                          ?? throw new InvalidOperationException("No such group registered");

            return GroupDto.FromGroup(group);
        }

        public StudentDto AddStudent(StudentDto studentDto)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == studentDto.Group.Name)
                ?? throw new InvalidOperationException("No such group registered");

            var student = new Student(studentDto.Name, group);
            _context.Students.Add(student);
            student.Group.Students.Add(student);
            _context.SaveChanges();
            return StudentDto.FromStudent(student);
        }

        public void DeleteStudent(Guid id)
        {
            Student student = _context.Students
                                  .Include(x => x.Group)
                                  .FirstOrDefault(x => x.Id == id)
                              ?? throw new InvalidOperationException("No such student registered");
            _context.SubjectGroups
                .Include(x => x.Students)
                .Where(x => x.Students.Contains(student))
                .ToList()
                .ForEach(x => x.Students.Remove(student));
            student.Group.Students.Remove(student);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public IReadOnlyList<StudentDto> GetStudents()
        {
            return _context.Students
                .Include(x => x.Group)
                .Select(x => StudentDto.FromStudent(x))
                .ToList();
        }

        public IReadOnlyList<StudentDto> GetGroupStudents(string groupName)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                          ?? throw new InvalidOperationException("No such group registered");

            return group.Students.Select(x => StudentDto.FromStudent(x)).ToList();
        }

        public StudentDto MoveStudent(Guid studentId, string groupName)
        {
            Student student = _context.Students
                                  .Include(x => x.Group)
                                  .ThenInclude(x => x.Students)
                                  .FirstOrDefault(x => x.Id == studentId)
                              ?? throw new InvalidOperationException("No such student registered");

            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                          ?? throw new InvalidOperationException("No such group registered");

            student.Group.Students.Remove(student);
            group.Students.Add(student);
            student.Group = group;
            _context.SaveChanges();
            return StudentDto.FromStudent(student);
        }

        public SubjectDto CreateSubject(SubjectDto subjectDto)
        {
            if (_context.Subjects.Find(subjectDto.Name) is not null)
            {
                throw new InvalidOperationException("Subject already exists");
            }

            var subject = new Subject(subjectDto.Name);
            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return SubjectDto.FromSubject(subject);
        }

        public void DeleteSubject(string subjectName)
        {
            if (_context.Reports.Include(x => x.Subject).Any(x => x.Subject.Name == subjectName))
            {
                throw new InvalidOperationException("Subject is used in report");
            }

            if (_context.SubjectGroups.Include(x => x.Subject).Any(x => x.Subject.Name == subjectName))
            {
                throw new InvalidOperationException("Subject is used in subject group");
            }

            Subject subject = _context.Subjects.Find(subjectName)
                              ?? throw new InvalidOperationException("No such subject registered");

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
        }

        public IReadOnlyList<SubjectDto> GetSubjects()
        {
            return _context.Subjects.Select(x => new SubjectDto(x.Name)).ToList();
        }

        public MinimalTeacherDto AddTeacher(MinimalTeacherDto minimalTeacherDto)
        {
            var teacher = new Teacher(minimalTeacherDto.FullName);
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return MinimalTeacherDto.FromTeacher(teacher);
        }

        public void DeleteTeacher(Guid id)
        {
            Teacher teacher = _context.Teachers
                                  .FirstOrDefault(x => x.Id == id)
                              ?? throw new InvalidOperationException("No such teacher registered");

            if (_context.SubjectGroups.Include(x => x.Teacher).Any(x => x.Teacher == teacher))
            {
                throw new InvalidOperationException("Teacher is used in subject group");
            }

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
        }

        public IReadOnlyList<MinimalTeacherDto> GetTeachers()
        {
            return _context.Teachers
                .Select(x => MinimalTeacherDto.FromTeacher(x))
                .ToList();
        }

        public TeacherDto GetTeacher(Guid id)
        {
            Teacher teacher = _context.Teachers
                .Include(x => x.SubjectGroups)
                .FirstOrDefault(x => x.Id == id)
                ?? throw new InvalidOperationException("No such teacher registered");
            return TeacherDto.FromTeacher(teacher);
        }

        public MinimalSubjectGroupDto AddSubjectGroup(MinimalSubjectGroupDto minimalSubjectGroupDto)
        {
            Subject subject = _context.Subjects
                              .FirstOrDefault(x => x.Name == minimalSubjectGroupDto.Subject.Name)
                          ?? throw new InvalidOperationException("No such subject registered");

            Teacher teacher = _context.Teachers
                              .FirstOrDefault(x => x.Id == minimalSubjectGroupDto.MinimalTeacherDto.Id)
                          ?? throw new InvalidOperationException("No such teacher registered");

            var subjectGroup = new SubjectGroup(teacher, subject);
            _context.SubjectGroups.Add(subjectGroup);
            _context.SaveChanges();
            return MinimalSubjectGroupDto.FromSubjectGroup(subjectGroup);
        }

        public void DeleteSubjectGroup(Guid id)
        {
            SubjectGroup subjectGroup = _context.SubjectGroups
                                          .Include(x => x.Students)
                                          .Include(x => x.Teacher)
                                          .FirstOrDefault(x => x.Id == id)
                                      ?? throw new InvalidOperationException("No such subject group registered");

            subjectGroup.Students.ForEach(student => subjectGroup.Students.Remove(student));
            subjectGroup.Teacher.SubjectGroups.Remove(subjectGroup);
            _context.SubjectGroups.Remove(subjectGroup);
            _context.SaveChanges();
        }

        public MinimalSubjectGroupDto AddStudentToSubjectGroup(Guid studenId, Guid groupId)
        {
            Student student = _context.Students
                              .FirstOrDefault(x => x.Id == studenId)
                          ?? throw new InvalidOperationException("No such student registered");

            SubjectGroup subjectGroup = _context.SubjectGroups
                                          .Include(x => x.Students)
                                          .Include(x => x.Teacher)
                                          .FirstOrDefault(x => x.Id == groupId)
                                      ?? throw new InvalidOperationException("No such subject group registered");

            if (subjectGroup.Students.Contains(student))
            {
                throw new InvalidOperationException("Student is already in subject group");
            }

            subjectGroup.Students.Add(student);
            _context.SaveChanges();
            return MinimalSubjectGroupDto.FromSubjectGroup(subjectGroup);
        }

        public void DeleteStudentFromSubjectGroup(Guid studentId, Guid groupId)
        {
            Student student = _context.Students
                                  .FirstOrDefault(x => x.Id == studentId)
                              ?? throw new InvalidOperationException("No such student registered");

            SubjectGroup subjectGroup = _context.SubjectGroups
                                            .Include(x => x.Students)
                                            .FirstOrDefault(x => x.Id == groupId)
                                        ?? throw new InvalidOperationException("No such subject group registered");

            if (!subjectGroup.Students.Contains(student))
            {
                throw new InvalidOperationException("Student is not in subject group");
            }

            subjectGroup.Students.Remove(student);
            _context.SaveChanges();
        }

        public IReadOnlyList<MinimalSubjectGroupDto> GetSubjectGroups()
        {
            return _context.SubjectGroups
                .Include(x => x.Teacher)
                .Include(x => x.Subject)
                .Select(x => MinimalSubjectGroupDto.FromSubjectGroup(x))
                .ToList();
        }

        public SubjectGroupDto GetSubjectGroup(Guid groupId)
        {
            SubjectGroup subjectGroup = _context.SubjectGroups
                                          .Include(x => x.Students)
                                          .ThenInclude(x => x.Group)
                                          .Include(x => x.Teacher)
                                          .FirstOrDefault(x => x.Id == groupId)
                                      ?? throw new InvalidOperationException("No such subject group registered");

            return SubjectGroupDto.FromSubjectGroup(subjectGroup);
        }
    }
}