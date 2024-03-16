using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Workspace : BaseEntity
{
    public string Name { get; private set; }
    public bool IsPublic { get; private set; }
    public List<Task> Tasks { get; private set; }
    public Workspace(string name, bool isPublic)
    {
        Name = name;
        Tasks = new List<Task>();
        IsPublic = isPublic;
    }

    public void UpdateWorkspace(string name, bool isPublic)
    {
        this.Name = name;
        this.IsPublic = isPublic;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if (obj.GetType() != this.GetType()) return false;

        return this.Equals((Workspace)obj);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode() + this.Name.GetHashCode();
    }

    public bool Equals(Workspace other)
    {
        if (other == null)
        {
            return false;
        }

        return this.Id.Equals(other.Id) && this.Name.Equals(other.Id) && this.IsPublic.Equals(other.IsPublic);
    }
}
