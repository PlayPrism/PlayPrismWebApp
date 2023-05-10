namespace PlayPrism.Contracts.V1.Responses.Products;

/// <summary>
/// Represe
/// </summary>
public class ProductResponse
{
    /// <summary>
    ///     Gets or sets product id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets Name of Product.
    /// </summary>
    public string Name { get; set; }
    
    public double Rating { get; set; }
    
    /// <summary>
    /// Gets or sets Price of Product.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Gets or sets header Image url.
    /// </summary>
    public string HeaderImage { get; set; }
    
    /// <summary>
    /// Gets or sets Description of Product.
    /// </summary>
    public string ShortDescription { get; set; }
    
    /// <summary>
    /// Gets or sets Description of Product.
    /// </summary>
    public string DetailedDescription { get; set; }
    
    /// <summary>
    /// Gets or sets release date.
    /// </summary>
    public DateTime ReleaseDate { get; set; }
    
    /// <summary>
    /// Gets or sets product genres.
    /// </summary>
    public IList<string> Genres { get; set; }
    
    /// <summary>
    /// Gets or sets product platforms.
    /// </summary>
    public IList<string> Platforms { get; set; }
}