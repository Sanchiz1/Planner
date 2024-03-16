namespace Web.Models.Workspace;

public class UpdateWorkspaceDto
{
    public int WorkspaceId { get; set; }
    public required string WorkspaceName { get; set; }
    public required bool WorkspaceIsPublic { get; set; }
}