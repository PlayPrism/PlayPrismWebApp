namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table ProductCategory in database.
/// </summary>
public class ProductCategory : BaseEntity
{
    /// <summary>
    /// Gets or sets name of Product Category.
    /// </summary>
    public string CategoryName { get; set; }
}