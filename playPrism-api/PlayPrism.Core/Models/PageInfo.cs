namespace PlayPrism.Core.Models;

/// <summary>
/// Class that defines pagination properties.
/// </summary>
public class PageInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageInfo"/> class.
    /// </summary>
    public PageInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PageInfo"/> class.
    /// </summary>
    /// <param name="size">Page size.</param>
    /// <param name="number">Page number.</param>
    public PageInfo(int size, int number)
    {
        Size = size;
        Number = number;
    }

    /// <summary>
    /// Gets or sets int property for page size.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets int property for number of the page.
    /// </summary>
    public int Number { get; set; }
}