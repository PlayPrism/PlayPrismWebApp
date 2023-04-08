namespace PlayPrism.Core.Domain;


public class ProductCategory : BaseEntity
{
    public Guid ParentCategoryId { get; set; }
    
    public ProductCategory ParentCategory { get; set; }
    
    public string Key { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; }
}