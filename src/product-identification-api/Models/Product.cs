namespace IntelligentProducts.ProductIdentificationApi.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public ProductTypes ProductType { get; set; }

    public DateTimeOffset IntroductionDate { get; set; }

    public DateTimeOffset SalesDiscontinuationDate { get; set; }

    public DateTimeOffset SupportDiscontinuationDate { get; set; }

    public ICollection<AlternateIdentity> AlternateIdentities { get; set; } = [];
}