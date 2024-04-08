using System.Text.Json.Serialization;

namespace IntelligentProducts.ProductIdentificationApi.Models;

public class AlternateIdentity
{
    public int ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public IdentityTypes IdentityType { get; set; }

    public string Value { get; set; } = default!;
}