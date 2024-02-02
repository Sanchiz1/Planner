using Domain.Common;
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; }
    public Role(string name)
    {
        Name = name;
    }

    public static Role OwnerRole => new Role(Roles.Owner);
    public static Role MemberRole => new Role(Roles.Member);
    public static Role ViewerRole => new Role(Roles.Viewer);
}
