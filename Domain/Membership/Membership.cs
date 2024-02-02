using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Membership;

public class Membership
{
    public int WorkspaceId { get; set; }
    public int RoleId { get; set; }
    public required Workspace Workspace { get; set; }
    public required Role Role { get; set; }
}