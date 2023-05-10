using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Products;
using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;

namespace PlayPrism.API.Controllers.V1;

/// <inheritdoc />
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="productsService">The product service.</param>
    /// <param name="mapper">The automapper service.</param>
    public ProductsController(
        IProductsService productsService,
        IMapper mapper)
    {
        _productsService = productsService;
        _mapper = mapper;
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
    /// <response code="200">Products</response>
    /// <response code="400">Bad request</response>
    [ProducesResponseType(typeof(IList<GetProductsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{category}")]
    public async Task<IActionResult> GetFilteredProductsAsync(
        [FromRoute] string category,
        [FromBody] GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        var res = await _productsService
                .GetProductsByFiltersWithPaginationAsync(
                    category,
                    request.PageInfo,
                    request.Filters,
                    cancellationToken);

        var response = _mapper.Map<List<GetProductsResponse>>(res);

        return Ok(response.ToApiListResponse());
    }

    /// <summary>
    /// Retrieves filters for selected category of products.
    /// </summary>
    /// <param name="category">The category name string.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{category}/filters")]
    public async Task<IActionResult> GetCategoryFiltersAsync([FromRoute] string category, CancellationToken cancellationToken)
    {
        var res = await _productsService
                .GetFilterForCategoryAsync(category, cancellationToken: cancellationToken);

        return Ok(res.ToApiListResponse());
    }
}