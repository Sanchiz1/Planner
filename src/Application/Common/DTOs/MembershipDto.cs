﻿namespace Application.Common.DTOs;

public class MembershipDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int WorkspaceId { get; set; }
    public int RoleId { get; set; }
    public RoleDto Role { get; set; }
    public WorkspaceDto Workspace { get; set; }
}