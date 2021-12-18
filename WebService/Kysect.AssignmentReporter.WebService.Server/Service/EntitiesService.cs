using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kysect.AssignmentReporter.WebService.DAL.Database;
using Kysect.AssignmentReporter.WebService.DAL.Entities;
using Kysect.AssignmentReporter.WebService.Shared;
using Kysect.AssignmentReporter.WebService.Shared.CreationalDto;
using Microsoft.EntityFrameworkCore;
using Group = Kysect.AssignmentReporter.WebService.DAL.Entities.Group;

namespace Kysect.AssignmentReporter.WebService.Server.Service
{
    public class EntitiesService
    {
        private readonly IMapper _mapper;
        private readonly AssignmentReporterContext _context;

        public EntitiesService(AssignmentReporterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MinimalGroupDto AddGroup(GroupCreationalDto groupDto)
        {
            if (_context.Groups.Any(g => g.Name == groupDto.Name))
            {
                throw new ArgumentException("Group already exists");
            }

            var group = new Group(groupDto.Name);
            _context.Groups.Add(new Group(groupDto.Name));
            _context.SaveChanges();
            return _mapper.Map<MinimalGroupDto>(group);
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
                .ProjectTo<MinimalGroupDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public GroupDto GetGroup(string groupName)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                          ?? throw new InvalidOperationException("No such group registered");

            return _mapper.Map<GroupDto>(group);
        }

        public StudentDto AddStudent(StudentCreationalDto studentDto)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == studentDto.GroupName)
                ?? throw new InvalidOperationException("No such group registered");

            var student = new Student(studentDto.FullName, group);
            _context.Students.Add(student);
            student.Group.Students.Add(student);
            _context.SaveChanges();
            return _mapper.Map<StudentDto>(student);
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
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public IReadOnlyList<StudentDto> GetGroupStudents(string groupName)
        {
            Group group = _context.Groups
                              .Include(x => x.Students)
                              .FirstOrDefault(x => x.Name == groupName)
                          ?? throw new InvalidOperationException("No such group registered");

            return group.Students
                .AsQueryable()
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToList();
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
            return _mapper.Map<StudentDto>(student);
        }

        public SubjectDto AddSubject(SubjectCreationalDto subjectDto)
        {
            if (_context.Subjects.Find(subjectDto.Name) is not null)
            {
                throw new InvalidOperationException("Subject already exists");
            }

            var subject = new Subject(subjectDto.Name);
            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return _mapper.Map<SubjectDto>(subject);
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

        public MinimalTeacherDto AddTeacher(TeacherCreationalDto minimalTeacherDto)
        {
            var teacher = new Teacher(minimalTeacherDto.FullName);
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return _mapper.Map<MinimalTeacherDto>(teacher);
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
                .ProjectTo<MinimalTeacherDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public TeacherDto GetTeacher(Guid id)
        {
            Teacher teacher = _context.Teachers
                .Include(x => x.SubjectGroups)
                .FirstOrDefault(x => x.Id == id)
                ?? throw new InvalidOperationException("No such teacher registered");
            return _mapper.Map<TeacherDto>(teacher);
        }

        public MinimalSubjectGroupDto AddSubjectGroup(SubjectGroupCreationalDto minimalSubjectGroupDto)
        {
            Subject subject = _context.Subjects
                              .FirstOrDefault(x => x.Name == minimalSubjectGroupDto.SubjectName)
                          ?? throw new InvalidOperationException("No such subject registered");

            Teacher teacher = _context.Teachers
                              .FirstOrDefault(x => x.Id == minimalSubjectGroupDto.TeacherId)
                          ?? throw new InvalidOperationException("No such teacher registered");

            var subjectGroup = new SubjectGroup(teacher, subject);
            _context.SubjectGroups.Add(subjectGroup);
            _context.SaveChanges();
            return _mapper.Map<MinimalSubjectGroupDto>(subjectGroup);
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

        public MinimalSubjectGroupDto AddStudentToSubjectGroup(Guid studentId, Guid groupId)
        {
            Student student = _context.Students
                              .FirstOrDefault(x => x.Id == studentId)
                          ?? throw new InvalidOperationException("No such student registered");

            SubjectGroup subjectGroup = _context.SubjectGroups
                                          .Include(x => x.Students)
                                          .Include(x => x.Teacher)
                                          .Include(x => x.Subject)
                                          .FirstOrDefault(x => x.Id == groupId)
                                      ?? throw new InvalidOperationException("No such subject group registered");

            if (subjectGroup.Students.Contains(student))
            {
                throw new InvalidOperationException("Student is already in subject group");
            }

            subjectGroup.Students.Add(student);
            _context.SaveChanges();
            return _mapper.Map<MinimalSubjectGroupDto>(subjectGroup);
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
                .ProjectTo<MinimalSubjectGroupDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public SubjectGroupDto GetSubjectGroup(Guid groupId)
        {
            SubjectGroup subjectGroup = _context.SubjectGroups
                                          .Include(x => x.Students)
                                          .ThenInclude(x => x.Group)
                                          .Include(x => x.Teacher)
                                          .Include(x => x.Subject)
                                          .FirstOrDefault(x => x.Id == groupId)
                                      ?? throw new InvalidOperationException("No such subject group registered");

            return _mapper.Map<SubjectGroupDto>(subjectGroup);
        }
    }
}