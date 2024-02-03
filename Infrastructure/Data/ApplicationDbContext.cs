using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Application.Common.Interfaces.IApplicationDbContext;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Task> Tasks => Set<Domain.Entities.Task>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<Role> MembershipRoles => Set<Role>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}