using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IntelligentProducts.ProductIdentificationApi.Models;

namespace IntelligentProducts.ProductIdentificationApi.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(2048);

        builder.Property(p => p.ProductType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.IntroductionDate)
            .IsRequired();

        builder.Property(p => p.SalesDiscontinuationDate)    
            .IsRequired();

        builder.Property(p => p.SupportDiscontinuationDate)
            .IsRequired();
    }
}