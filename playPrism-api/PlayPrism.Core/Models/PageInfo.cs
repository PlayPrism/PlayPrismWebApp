namespace PlayPrism.Core.Models;

/// <summary>
/// Class that defines pagination properties.
/// </summary>
public class PageInfo
{
    /// <summary>
    /// Gets or sets int property for page size.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets int property for number of the page.
    /// </summary>
    public int Number { get; set; }
}