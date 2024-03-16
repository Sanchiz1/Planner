using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations;
public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Membership> builder)
    {
        builder
            .HasKey(bc => new { bc.Id });

        builder.HasIndex(bc => new { bc.WorkspaceId, bc.UserId }).IsUnique();
        
        builder
            .Property(bc => bc.UserId)
            .HasColumnName("UserId");

        builder
            .HasOne(bc => bc.Workspace)
            .WithMany()
            .HasForeignKey(bc => bc.WorkspaceId);

        builder
            .HasOne(bc => bc.Role)
            .WithMany()
            .HasForeignKey(bc => bc.RoleId);
    }
}
