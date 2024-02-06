namespace Web.Models.Workspace;

public class RemoveFromWorkspaceDto
{
    public int MembershipId { get; set; }
    public int ToRemoveMembershipId { get; set; }
}