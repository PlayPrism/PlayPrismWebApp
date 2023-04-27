using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;

namespace PlayPrism.API.Controllers;

[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="productService">The product service.</param>
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// The index action.
    /// </summary>
    /// <param name="filters">Filters</param>
    /// <param name="pageInfo">Page info</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains the <see cref="IActionResult"/>.
    /// </returns>
    /// <response code="200">Products</response>
    /// <response code="400">Bad request</response>
    [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> Index(
        IList<Filter> filters,
        PageInfo pageInfo,
        CancellationToken cancellationToken)
    {
        try
        {
            var res = await _productService.GetProductsByFiltersWithPaginationAsync(filters, pageInfo,
                cancellationToken);
            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
}