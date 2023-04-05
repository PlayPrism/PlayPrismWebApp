namespace PlayPrism.Core.Domain;

public class Product : BaseEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public decimal Price { get; set; }
    
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    
    public IEnumerable<ProductItem> ProductItems { get; set; }
    
    public IEnumerable<UserReview> UserReviews { get; set; }
    
    public IEnumerable<ProductConfiguration> ProductConfigurations { get; set; }
}