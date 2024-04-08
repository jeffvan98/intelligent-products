using Microsoft.Identity.Client;

namespace IntelligentProducts.ProductIdentificationApi.Contracts;

public class ProductDTO
{
   public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public ProductTypesDTO ProductType { get; set; }

    public DateTimeOffset IntroductionDate { get; set; }

    public DateTimeOffset SalesDiscontinuationDate { get; set; }

    public DateTimeOffset SupportDiscontinuationDate { get; set; }

    public ICollection<AlternateIdentityDTO> AlternateIdentities { get; set; } = [];

}