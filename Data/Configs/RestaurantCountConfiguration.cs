using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Configs;

public class RestaurantCountConfiguration : IEntityTypeConfiguration<RestaurantCount>
{
    public void Configure(EntityTypeBuilder<RestaurantCount> builder)
    {
        builder.ToTable("RestaurantCounts", "TutorialShnp").HasKey(rc => rc.UserId);
    }
}
