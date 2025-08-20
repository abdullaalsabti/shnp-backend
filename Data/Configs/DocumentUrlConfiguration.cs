using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Configs;

public class DocumentUrlConfiguration : IEntityTypeConfiguration<DocumentUrl>
{
    public void Configure(EntityTypeBuilder<DocumentUrl> builder)
    {
        builder.ToTable("DocumentUrls", "TutorialShnp").HasKey(u => u.UrlId);
    }
}
