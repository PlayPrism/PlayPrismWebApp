using PlayPrism.Contracts.V1.Responses.ProductCatalogueResponses;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;

namespace PlayPrism.BLL.Abstractions.Interface;

/// <summary>
/// Product service that works with products filtering and pagination.
/// </summary>
public interface ICatalogueService
{
    /// <summary>
    /// Asynchronously get product by filter with pagination.
    /// </summary>
    /// <param name="filters">List of filters.</param>
    /// <param name="pageInfo">Page information with size and number.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task result contains the list of products.</returns>
    // Task GetProductsByFiltersWithPaginationAsync(
    //     IEnumerable<Filter> filters,
    //     PageInfo pageInfo,
    //     CancellationToken cancellationToken);
    Task<IList<Product>> GetProductsByFiltersWithPaginationAsync(
        IEnumerable<Filter> filters,
        PageInfo pageInfo,
        CancellationToken cancellationToken);


    Task<IEnumerable<CategoryFiltersResponse>> GetFilterForCategoryAsync(
        string category,
        CancellationToken cancellationToken);
}