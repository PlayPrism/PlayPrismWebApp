namespace PlayPrism.Core.Domain.Filters;

/// <summary>
/// Filter model that represents Name-Value pair and is applied for filtering.
/// </summary>
public class Filter
{
    /// <summary>
    /// Gets or sets represents Name of the filter.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets represents Value of the filter.
    /// </summary>
    public string Value { get; set; }
}