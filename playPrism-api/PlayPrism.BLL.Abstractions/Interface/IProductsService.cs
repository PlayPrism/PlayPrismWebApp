using PlayPrism.Contracts.V1.Requests.Products;
using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;

namespace PlayPrism.BLL.Abstractions.Interface;

/// <summary>
/// Product service that works with products filtering and pagination.
/// </summary>
public interface IProductsService
{
    /// <summary>
    /// Asynchronously get product by filter with pagination.
    /// </summary>
    /// <param name="category">The product category.</param>
    /// <param name="pageInfo">The pagination parameters.</param>
    /// <param name="filters">The filters parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task result contains the list of products.</returns>
    // Task GetProductsByFiltersWithPaginationAsync(
    //     IEnumerable<Filter> filters,
    //     PageInfo pageInfo,
    //     CancellationToken cancellationToken);
    Task<IList<Product>> GetProductsByFiltersWithPaginationAsync(
        string category,
        PageInfo pageInfo,
        Filter[] filters,
        CancellationToken cancellationToken);


    /// <summary>
    /// Asynchronously returns the list of filters that can be appliend to specific category.
    /// </summary>
    /// <param name="category">The product category.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to cancel task completion.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<IEnumerable<ProductConfiguration>> GetFilterForCategoryAsync(
        string category,
        CancellationToken cancellationToken);
}