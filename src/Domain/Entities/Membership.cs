﻿using Domain.Common;

namespace Domain.Entities;

public class Membership : BaseEntity
{
    public int UserId { get; private set; }
    public int WorkspaceId { get; private set; }
    public int RoleId { get; private set; }
    public Workspace Workspace { get; private set; }
    public Role Role { get; private set; }

    private Membership() { }

    public bool IsMembershipOwner(int userId)
    {
        return UserId == userId;
    }

    public static Membership CreateWorkspace(int userId, string workspaceName)
    {
        return new Membership()
        {
            UserId = userId,
            Workspace = new Workspace(workspaceName),
            RoleId = Role.OwnerRole.Id,
            Role = Role.OwnerRole
        };
    }
    
    public static Membership AddToWorkspace(int userId, Workspace workspace, int roleId)
    {
        if (roleId == Role.OwnerRole.Id)
        {
            throw new ArgumentException("Cannot add another owner to workspace");
        }
        
        return new Membership()
        {
            UserId = userId,
            Workspace = workspace,
            RoleId = roleId
        };
    }
}