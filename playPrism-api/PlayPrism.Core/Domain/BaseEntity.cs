namespace PlayPrism.Core.Domain;

/// <summary>
///     Represents base entity class
/// </summary>
public class BaseEntity
{
    /// <summary>
    ///     Gets or sets entity id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets date created time for entity
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets date updated for entity
    /// </summary>
    public DateTime DateUpdated { get; set; }
}