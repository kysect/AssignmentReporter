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

        public void AddGroup(string groupName)
        {
            var group = new Group(groupName);
            if (_context.Groups.Any(g => g.Name == groupName))
            {
                throw new ArgumentException("Group already exists");
            }

            _context.Groups.Add(group);
            _context.SaveChanges();
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

        public GroupDto GetGroup(string groupName)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                          ?? throw new InvalidOperationException("No such group registered");

            return GroupDto.FromGroup(group);
        }

        public IReadOnlyList<string> GetGroupNames()
        {
            return _context.Groups.Select(x => x.Name).ToList();
        }

        public void AddStudent(string fullName, string groupName)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                ?? throw new InvalidOperationException("No such group registered");

            var student = new Student(fullName, group);
            student.Group.Students.Add(student);
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public StudentDto GetStudent(Guid id)
        {
            Student student = _context.Students
                              .Include(x => x.Group)
                              .FirstOrDefault(x => x.Id == id)
                ?? throw new InvalidOperationException("No such student registered");

            return StudentDto.FromStudent(student);
        }

        public void DeleteStudent(Guid studentId)
        {
            Student student = _context.Students
                                  .Include(x => x.Group)
                                  .FirstOrDefault(x => x.Id == studentId)
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

        public void MoveStudent(Guid studentId, string newGroupName)
        {
            Student student = _context.Students
                                  .Include(x => x.Group)
                                  .ThenInclude(x => x.Students)
                                  .FirstOrDefault(x => x.Id == studentId)
                              ?? throw new InvalidOperationException("No such student registered");

            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == newGroupName)
                          ?? throw new InvalidOperationException("No such group registered");

            student.Group.Students.Remove(student);
            group.Students.Add(student);
            student.Group = group;
            _context.SaveChanges();
        }

        public void CreateSubject(string name)
        {
            if (_context.Subjects.Find(name) is not null)
            {
                throw new InvalidOperationException("Subject already exists");
            }

            _context.Subjects.Add(new Subject(name));
            _context.SaveChanges();
        }

        public void DeleteSubject(string name)
        {
            if (_context.Reports.Include(x => x.Subject).Any(x => x.Subject.Name == name))
            {
                throw new InvalidOperationException("Subject is used in report");
            }

            if (_context.SubjectGroups.Include(x => x.Subject).Any(x => x.Subject.Name == name))
            {
                throw new InvalidOperationException("Subject is used in subject group");
            }

            Subject subject = _context.Subjects.Find(name)
                              ?? throw new InvalidOperationException("No such subject registered");

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
        }

        public IReadOnlyList<SubjectDto> GetSubjects()
        {
            return _context.Subjects.Select(x => new SubjectDto(x.Name)).ToList();
        }

        public void AddTeacher(string fullName)
        {
            _context.Teachers.Add(new Teacher(fullName));
            _context.SaveChanges();
        }

        public void DeleteTeacher(Guid teacherId)
        {
            Teacher teacher = _context.Teachers
                                  .FirstOrDefault(x => x.Id == teacherId)
                              ?? throw new InvalidOperationException("No such teacher registered");

            if (_context.SubjectGroups.Include(x => x.Teacher).Any(x => x.Teacher == teacher))
            {
                throw new InvalidOperationException("Teacher is used in subject group");
            }

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
        }

        public IReadOnlyList<TeacherDto> GetTeachers()
        {
            return _context.Teachers
                .Include(x => x.SubjectGroups)
                .Select(x => TeacherDto.FromTeacher(x))
                .ToList();
        }

        public void AddSubjectGroup(string subjectName, Guid teacherId)
        {
            Subject subject = _context.Subjects
                                  .FirstOrDefault(x => x.Name == subjectName)
                              ?? throw new InvalidOperationException("No such subject registered");

            Teacher teacher = _context.Teachers
                                  .FirstOrDefault(x => x.Id == teacherId)
                              ?? throw new InvalidOperationException("No such teacher registered");

            _context.SubjectGroups.Add(new SubjectGroup(teacher, subject));
            _context.SaveChanges();
        }

        public void DeleteSubjectGroup(Guid id)
        {
            SubjectGroup subjectGroup = _context.SubjectGroups
                                          .Include(x => x.Students)
                                          .Include(x => x.Teacher)
                                          .FirstOrDefault(x => x.Id == id)
                                      ?? throw new InvalidOperationException("No such subject group registered");

            subjectGroup.Teacher.SubjectGroups.Remove(subjectGroup);
            _context.SubjectGroups.Remove(subjectGroup);
            _context.SaveChanges();
        }

        public void AddStudentToSubjectGroup(Guid studentId, Guid subjectGroupId)
        {
            Student student = _context.Students
                                  .FirstOrDefault(x => x.Id == studentId)
                              ?? throw new InvalidOperationException("No such student registered");

            SubjectGroup subjectGroup = _context.SubjectGroups
                                          .Include(x => x.Students)
                                          .Include(x => x.Teacher)
                                          .FirstOrDefault(x => x.Id == subjectGroupId)
                                      ?? throw new InvalidOperationException("No such subject group registered");

            subjectGroup.Students.Add(student);
            _context.SaveChanges();
        }

        public void RemoveStudentFromSubjectGroup(Guid studentId, Guid subjectGroupId)
        {
            Student student = _context.Students
                                  .FirstOrDefault(x => x.Id == studentId)
                              ?? throw new InvalidOperationException("No such student registered");

            SubjectGroup subjectGroup = _context.SubjectGroups
                                            .Include(x => x.Students)
                                            .Include(x => x.Teacher)
                                            .FirstOrDefault(x => x.Id == subjectGroupId)
                                        ?? throw new InvalidOperationException("No such subject group registered");

            subjectGroup.Students.Remove(student);
            _context.SaveChanges();
        }

        public IReadOnlyList<SubjectGroupDto> GetSubjectGroups()
        {
            return _context.SubjectGroups
                .Include(x => x.Students)
                .ThenInclude(x => x.Group)
                .Include(x => x.Teacher)
                .Include(x => x.Subject)
                .Select(x => SubjectGroupDto.FromSubjectGroup(x))
                .ToList();
        }
    }
}