namespace Web.Models.Workspace;

public record UpdateWorkspaceMemberRoleDto
{
    public int ToUpdateMembershipId { get; init; }
    public int ToUpdateRoleId { get; init; }
}
