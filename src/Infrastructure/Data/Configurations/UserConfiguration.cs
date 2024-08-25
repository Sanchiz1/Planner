using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApplicationUser> builder)
    {

        builder
            .HasMany(u => u.Memberships)
            .WithOne()
            .HasForeignKey(t => t.UserId);
    }
}
