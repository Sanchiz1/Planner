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
    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public Role(string name)
    {
        Name = name;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if (obj.GetType() != this.GetType()) return false;

        return this.Equals((Role)obj);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode() + this.Name.GetHashCode();
    }

    public bool Equals(Role other)
    {
        if (other == null)
        {
            return false;
        }

        return this.Id.Equals(other.Id) && this.Name.Equals(other.Id);
    }

    public static Role OwnerRole => new Role(1, Roles.Owner);
    public static Role MemberRole => new Role(2, Roles.Member);
    public static Role ViewerRole => new Role(3, Roles.Viewer);

    public static bool IsOwnerRole(int roleId) => OwnerRole.Id == roleId;
    public static bool IsMemberRole(int roleId) => MemberRole.Id == roleId;
    public static bool IsViewerRole(int roleId) => ViewerRole.Id == roleId;
}
