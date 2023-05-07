using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.V1.Requests.ProductCatalogueRequests;
using PlayPrism.Contracts.V1.Responses.ProductCatalogueResponses;
using PlayPrism.Core.Domain;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

using System.Linq.Expressions;

/// <inheritdoc />
public class CatalogueService : ICatalogueService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CatalogueService"/> class.
    /// </summary>
    /// <param name="unitOfWork">Unit of work di.</param>
    public CatalogueService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<IList<Product>> GetProductsByFiltersWithPaginationAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var predicates = new List<Expression<Func<Product, bool>>>();

            if (request?.Filters != null)
            {
                foreach (var filter in request.Filters)
                {
                    predicates.Add(
                        product => product.VariationOptions.Any(option =>
                            option.Value == filter.Value &&
                            option.ProductConfiguration.ConfigurationName == filter.Name));
                }
            }

            predicates.Add(product => product.ProductCategory.CategoryName == request.Category);

            Expression<Func<Product, Product>> selector = q => new Product
            {
                Name = q.Name,
                VariationOptions = q.VariationOptions.Select(option => new VariationOption
                {
                    Id = option.Id,
                    ProductConfiguration = option.ProductConfiguration,
                    Value = option.Value,
                }).ToList(),
                Description = q.Description,
                Id = q.Id,
                ProductCategory = q.ProductCategory,
                ProductCategoryId = q.ProductCategoryId,
                Image = q.Image,
                Price = q.Price,
            };

            var res = await this._unitOfWork.Products
                .GetPageWithMultiplePredicatesAsync(predicates, request.PageInfo, selector, cancellationToken);

            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryFiltersResponse>> GetFilterForCategoryAsync(string category,
        CancellationToken cancellationToken)
    {
        var categoryConfigurations = await this._unitOfWork.ProductConfigurations
            .GetByConditionAsync(
                configuration => configuration.Category.CategoryName == category,
                configuration => new ProductConfiguration
                {
                    Id = configuration.Id,
                    VariationOptions = configuration.VariationOptions,
                    Category = configuration.Category,
                    ConfigurationName = configuration.ConfigurationName,
                }, cancellationToken);

        var filters = categoryConfigurations
            .Select(configuration => new CategoryFiltersResponse
            {
                Title = configuration.ConfigurationName,
                FilterOptions = configuration.VariationOptions.Select(option => option.Value).Distinct().ToArray(),
            });

        return filters;
    }
}