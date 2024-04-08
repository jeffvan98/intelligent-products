using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IntelligentProducts.ProductIdentificationApi.Models;

namespace IntelligentProducts.ProductIdentificationApi.Configuration;

public class AlternateIdentityConfiguration : IEntityTypeConfiguration<AlternateIdentity>
{
    public void Configure(EntityTypeBuilder<AlternateIdentity> builder)
    {
        builder.HasKey(ai => new {ai.ProductId, ai.IdentityType});
        
        builder.Property(ai => ai.IdentityType)
            .HasConversion<string>();

        builder.Property(ai => ai.Value)
            .IsRequired();

        builder.HasOne(ai => ai.Product)
            .WithMany(p => p.AlternateIdentities)
            .HasForeignKey(ai => ai.ProductId)
            .IsRequired();
    }
}