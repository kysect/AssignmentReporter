using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public class MinimalGroupDto
{
    public MinimalGroupDto(string name)
    {
        this.Name = name;
    }

    [Required]
    public string Name { get; set; }
    public static MinimalGroupDto FromGroup(Group group)
    {
        return new MinimalGroupDto(group.Name);
    }

    public static MinimalGroupDto FromGroupDto(GroupDto group)
    {
        return new MinimalGroupDto(group.Name);
    }
}