using Domain.Common;

namespace Domain.Entities;

public class Membership : BaseEntity
{
    public int UserId { get; private set; }
    public int WorkspaceId { get; private set; }
    public int RoleId { get; private set; }
    public Workspace Workspace { get; private set; }
    public Role Role { get; private set; }

    public Membership(int userId, int workspaceId, int roleId)
    {
        UserId = userId;
        WorkspaceId = workspaceId;
        RoleId = roleId;
    }
}