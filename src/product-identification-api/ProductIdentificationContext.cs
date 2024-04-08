using System.Reflection;
using Microsoft.EntityFrameworkCore;
using IntelligentProducts.ProductIdentificationApi.Models;

public class ProductIdentificationContext : DbContext
{
    public DbSet<Product> Products { get; set; } = default!;
    
    public DbSet<AlternateIdentity> AlternateIdentities { get; set; } = default!;

    public ProductIdentificationContext(DbContextOptions<ProductIdentificationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}