using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

using System.Linq.Expressions;

/// <inheritdoc />
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="unitOfWork">Unit of work di.</param>
    public ProductService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<IList<Product>> GetProductsByFiltersWithPaginationAsync(
        IEnumerable<Filter> filters,
        PageInfo pageInfo,
        CancellationToken cancellationToken)
    {
        try
        {
            var predicates = new List<Expression<Func<Product, bool>>>();

            foreach (var filter in filters)
            {
                predicates.Add(
                    product => product.ProductConfigurations.Any(configuration =>
                        configuration.ConfigurationName == filter.Name &&
                        configuration.VariationOption.Values.Contains(filter.Value)));
            }

            Expression<Func<Product, Product>> selector = q => new Product
            {
                Name = q.Name,
                ProductConfigurations = q.ProductConfigurations,
                Description = q.Description,
                Id = q.Id,
                Image = q.Image,
                Price = q.Price,
                DateCreated = q.DateCreated,
                DateUpdated = q.DateUpdated,
                ProductItems = q.ProductItems,
                UserReviews = q.UserReviews,
                ProductCategory = q.ProductCategory,
                ProductCategoryId = q.ProductCategoryId,
            };

            var res = await this._unitOfWork.Products
                .GetPageWithMultiplePredicatesAsync(predicates, pageInfo, selector, cancellationToken);

            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}