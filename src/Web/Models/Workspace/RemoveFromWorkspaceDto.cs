namespace Web.Models.Workspace;

public class RemoveFromWorkspaceDto
{
    public int WorkspaceId { get; set; }
    public int ToRemoveMembershipId { get; set; }
}