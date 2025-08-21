using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities.Random;

namespace WebApplication1.Data.Configs;

public class TopClubsConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.ToTable("Clubs", "TutorialShnp").HasKey(x => x.Id);
        builder.HasData(
            new Club
            {
                Id = 1,
                Name = "Bayern",
                Rating = 5.0,
                Description = "Bayern",
                PlayedMatches = 210
            },
            new Club
            {
                Id = 2,
                Name = "Dortmund",
                Rating = 4.7,
                Description = "Dortmund",
                PlayedMatches = 110
            },
            new Club
            {
                Id = 3,
                Name = "Madrid",
                Rating = 5.0,
                Description = "Madrid",
                PlayedMatches = 250
            });
    }
}
