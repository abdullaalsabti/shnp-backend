using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Configs;

public class RestaurantTypeConfiguration : IEntityTypeConfiguration<RestaurantType>
{
    public void Configure(EntityTypeBuilder<RestaurantType> builder)
    {
        builder.ToTable("RestaurantTypes", "TutorialShnp").HasKey(u => u.TypeId);
        builder.HasIndex(rt => rt.Name).IsUnique();
        builder.HasData(
            new RestaurantType { TypeId = 1, Name = "Fast Food" },
            new RestaurantType { TypeId = 2, Name = "Fine Dining" },
            new RestaurantType { TypeId = 3, Name = "Arabic" }
        );
    }
}
