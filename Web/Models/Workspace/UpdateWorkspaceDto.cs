namespace Web.Models.Workspace;

public class UpdateWorkspaceDto
{
    public int MembershipId { get; set; }
    public required string WorkspaceName { get; set; }
}