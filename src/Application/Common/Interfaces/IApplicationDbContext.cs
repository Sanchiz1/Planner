using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Task> Tasks { get; }
    DbSet<Tag> Tags { get; }
    DbSet<Workspace> Workspaces { get; }
    DbSet<Membership> Memberships { get; }
    DbSet<Role> MembershipRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
