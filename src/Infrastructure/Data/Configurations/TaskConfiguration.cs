using Microsoft.EntityFrameworkCore;

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
