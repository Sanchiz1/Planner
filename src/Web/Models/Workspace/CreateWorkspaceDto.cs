namespace Web.Models.Workspace;

public class CreateWorkspaceDto
{
    public required string WorkspaceName { get; set; }
    public required bool WorkspaceIsPublic { get; set; }
}