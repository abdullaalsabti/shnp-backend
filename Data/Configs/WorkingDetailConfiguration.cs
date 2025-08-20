using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Configs;

public class WorkingDetailConfiguration : IEntityTypeConfiguration<WorkingDetail>
{
    public void Configure(EntityTypeBuilder<WorkingDetail> builder)
    {
        builder.ToTable("WorkingDetails", "TutorialShnp").HasKey(u => u.WorkingDetailId);
    }
}
