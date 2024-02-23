namespace Web.Models.Workspace;

public class AddToWorkspaceDto
{
    public int MembershipId { get; set; }
    public int ToAddUserId { get; set; }
    public int ToAddRoleId { get; set; }
}