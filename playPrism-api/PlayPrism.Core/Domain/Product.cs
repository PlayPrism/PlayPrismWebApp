namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table Product in database.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets Name of Product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets Description of Product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets Image url of Product.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Gets or sets Price of Product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets FK id to ProductCategory entity.
    /// </summary>
    public Guid ProductCategoryId { get; set; }

    /// <summary>
    /// Gets or sets reference to ProductCategory entity.
    /// </summary>
    public ProductCategory ProductCategory { get; set; }

    /// <summary>
    /// Gets or sets references to ProductItems entities.
    /// </summary>
    public IList<ProductItem> ProductItems { get; set; }

    /// <summary>
    /// Gets or sets references to UserReviews entities.
    /// </summary>
    public IList<UserReview> UserReviews { get; set; }

    /// <summary>
    /// Gets or sets references to ProductConfigurations entities.
    /// </summary>
    public IList<VariationOption> VariationOptions { get; set; }
}