namespace PlayPrism.Contracts.V1.Responses.Products;

public class GetProductsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }
    public decimal Price { get; set; }
    public double Discount { get; set; }
    public string Description { get; set; }
    public IList<string> Genres { get; set; }
    public string Image { get; set; }
    public IList<string> Platforms { get; set; }
}