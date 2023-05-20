using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Products;
using PlayPrism.Contracts.V1.Responses.Products;

namespace PlayPrism.API.Controllers.V1;

/// <inheritdoc />
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly ILogger<ProductsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="productsService"><see cref="IProductsService"/></param>
    /// <param name="logger"><see cref="ILogger{TCategoryName}"/></param>
    public ProductsController(
        IProductsService productsService, ILogger<ProductsController> logger)
    {
        _productsService = productsService;
        _logger = logger;
    }

    /// <summary>
    /// Get products by filters.
    /// </summary>
    /// <param name="category">The category name.</param>
    /// <param name="request">The product request.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains the <see cref="IActionResult"/>.
    /// </returns>
    [HttpGet("{category}")]
    [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFilteredProductsAsync(
        [FromRoute] string category,
        [FromQuery] GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _productsService
                .GetProductsByFiltersWithPaginationAsync(
                    category,
                    request.PageInfo,
                    request.Filters,
                    cancellationToken);

        if (products is null)
        {
            _logger.LogError("Products not found");
            return NotFound("Products not found".ToErrorResponse());
        }

        return Ok(products.ToApiListResponse());
    }

    /// <summary>
    /// Retrieves filters for selected category of products.
    /// </summary>
    /// <param name="category">The category name string.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{category}/filters")]
    [ProducesResponseType(typeof(CategoryFiltersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryFiltersAsync([FromRoute] string category, CancellationToken cancellationToken)
    {
        var categoryFilters = await _productsService
                .GetFilterForCategoryAsync(category, cancellationToken: cancellationToken);

        if (categoryFilters is null)
        {
            _logger.LogError($"{category} filters not found");
            return NotFound($"{category} filters not found".ToErrorResponse());
        }

        return Ok(categoryFilters.ToApiListResponse());
    }

    /// <summary>
    /// Retrieves product by id.
    /// </summary>
    /// <param name="category">The category's name string</param>
    /// <param name="id">The product's id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains the <see cref="IActionResult"/>.
    /// </returns>
    [HttpGet("{category}/{id}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductByIdAsync(
        [FromRoute] string category,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var product = await _productsService
            .GetProductByIdAsync(category, id, cancellationToken);

        if (product is null)
        {
            _logger.LogError($"Product with {id} id in {category} category not found");
            return NotFound($"Product with {id} id in {category} category not found".ToErrorResponse());
        }

        return Ok(product.ToApiResponse());
    }
}