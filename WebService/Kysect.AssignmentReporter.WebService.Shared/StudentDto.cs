using System;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared
{
    public class StudentDto
    {
        public StudentDto(Guid id, string name, MinimalGroupDto @group)
        {
            this.Id = id;
            this.Name = name;
            this.Group = @group;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public MinimalGroupDto Group { get; set; }
        public static StudentDto FromStudent(Student student)
                    => new StudentDto(student.Id, student.FullName, MinimalGroupDto.FromGroup(student.Group));
    }
}