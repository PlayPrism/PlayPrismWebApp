using PlayPrism.Core.Domain;

namespace PlayPrism.BLL.Abstractions.Interface;

public class IUserService
{
    /// <summary>
    /// Asynchronously returns product by id.
    /// </summary>
    /// <param name="category">The product's category</param>
    /// <param name="id">The product's id</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to cancel task completion.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains product</returns>
    Task<UserProfile> GetUserByIdAsync(
        string category,
        Guid id,
        CancellationToken cancellationToken);
}