namespace IntelligentProducts.ProductIdentificationApi.Contracts;

public class AlternateIdentityDTO
{
    public IdentityTypesDTO IdentityType { get; set; }

    public string Value { get; set; } = default!;
}