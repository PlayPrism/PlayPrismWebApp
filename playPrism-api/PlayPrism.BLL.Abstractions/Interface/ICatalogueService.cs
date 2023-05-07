using PlayPrism.Contracts.V1.Requests.ProductCatalogueRequests;
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
    /// <param name="request">The product request object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task result contains the list of products.</returns>
    // Task GetProductsByFiltersWithPaginationAsync(
    //     IEnumerable<Filter> filters,
    //     PageInfo pageInfo,
    //     CancellationToken cancellationToken);
    Task<IList<Product>> GetProductsByFiltersWithPaginationAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken);


    /// <summary>
    /// Asynchronously returns the list of filters that can be appliend to specific category.
    /// </summary>
    /// <param name="category">The product category.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to cancel task completion.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<IEnumerable<CategoryFiltersResponse>> GetFilterForCategoryAsync(
        string category,
        CancellationToken cancellationToken);
}