using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations;

internal class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(1000);
    }
}
