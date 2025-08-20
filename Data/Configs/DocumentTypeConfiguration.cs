using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Configs;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable("DocumentTypeCodes", "TutorialShnp").HasKey(dt => dt.TypeCode);
        builder.HasData(
            new DocumentType { TypeCode = "IBAN", Type = "Tax Registration" },
            new DocumentType { TypeCode = "LR", Type = "License Registration" },
            new DocumentType { TypeCode = "NID", Type = "National ID" },
            new DocumentType { TypeCode = "CR", Type = "Certificate Registration" }
        );
    }
}
