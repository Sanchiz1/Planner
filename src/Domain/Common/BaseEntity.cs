using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common;

public abstract class BaseEntity : IEquatable<BaseEntity>
{
    public int Id { get; set; }

    public override bool Equals(object obj)
    {
        var entity = obj as BaseEntity;
        if (entity != null)
        {
            return this.Equals(entity);
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public bool Equals(BaseEntity other)
    {
        if (other == null)
        {
            return false;
        }

        return this.Id.Equals(other.Id);
    }
}
