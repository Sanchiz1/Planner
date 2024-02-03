using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Task> Tasks { get; }
    DbSet<Tag> Tags { get; }
    DbSet<Workspace> Workspaces { get; }
    DbSet<Membership> Memberships { get; }
    DbSet<Role> MembershipRoles { get; }
    DbSet<Post> Posts { get; }

    public class Post
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
