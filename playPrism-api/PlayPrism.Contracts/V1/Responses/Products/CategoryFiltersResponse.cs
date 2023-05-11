namespace PlayPrism.Contracts.V1.Responses.Products;

public class CategoryFiltersResponse
{
    public Guid Id { get; set; }
    public string ConfigurationName { get; set; }
    public string[] FilterOptions { get; set; }
}