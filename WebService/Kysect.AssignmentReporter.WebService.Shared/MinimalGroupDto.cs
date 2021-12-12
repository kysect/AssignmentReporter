using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record MinimalGroupDto(string Name)
{
    public static MinimalGroupDto FromGroup(Group group)
    {
        return new MinimalGroupDto(group.Name);
    }
}