using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Role> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        /*builder.HasData(
            new Role(Roles.Owner) { Id = 1},
            new Role(Roles.Member) { Id = 2},
            new Role(Roles.Viewer) { Id = 3}
            );*/
    }
}
