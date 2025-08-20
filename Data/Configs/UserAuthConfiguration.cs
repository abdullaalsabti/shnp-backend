using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Configs;

public class UserAuthConfiguration : IEntityTypeConfiguration<UserAuth>
{
    public void Configure(EntityTypeBuilder<UserAuth> builder)
    {
        builder.ToTable("UserAuth", "TutorialShnp").HasKey(u => u.UserId);
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
