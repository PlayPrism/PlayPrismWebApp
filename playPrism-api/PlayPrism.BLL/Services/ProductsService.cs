using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

using System.Linq.Expressions;

/// <inheritdoc />
public class ProductsService : IProductsService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsService"/> class.
    /// </summary>
    /// <param name="unitOfWork">Unit of work di.</param>
    public ProductsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<IList<Product>> GetProductsByFiltersWithPaginationAsync(
        string category,
        PageInfo pageInfo,
        Filter[] filters,
        CancellationToken cancellationToken)
    {
        try
        {
            var predicates = new List<Expression<Func<Product, bool>>>();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    predicates.Add(
                        product => product.VariationOptions.Any(option =>
                            option.Value == filter.Value &&
                            option.ProductConfiguration.ConfigurationName == filter.Name));
                }
            }

            predicates.Add(product => product.ProductCategory.CategoryName == category);

            Expression<Func<Product, Product>> selector = q => new Product
            {
                Name = q.Name,
                VariationOptions = q.VariationOptions.Select(option => new VariationOption
                {
                    Id = option.Id,
                    ProductConfiguration = option.ProductConfiguration,
                    Value = option.Value,
                }).ToList(),
                ShortDescription = q.ShortDescription,
                DetailedDescription = q.DetailedDescription,
                Id = q.Id,
                ProductCategory = q.ProductCategory,
                ProductCategoryId = q.ProductCategoryId,
                HeaderImage = q.HeaderImage,
                ReleaseDate = q.ReleaseDate,
                Price = q.Price,
            };

            var res = await _unitOfWork.Products
                .GetPageWithMultiplePredicatesAsync(predicates, pageInfo, selector, cancellationToken);

            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductConfiguration>> GetFilterForCategoryAsync(string category,
        CancellationToken cancellationToken)
    {
        var categoryConfigurations = await _unitOfWork.ProductConfigurations
            .GetByConditionAsync(
                configuration => configuration.Category.CategoryName == category,
                configuration => new ProductConfiguration
                {
                    Id = configuration.Id,
                    VariationOptions = configuration.VariationOptions,
                    Category = configuration.Category,
                    ConfigurationName = configuration.ConfigurationName,
                }, cancellationToken);

        return categoryConfigurations;
    }

    /// <inheritdoc />
    public async Task<Product> GetProductByIdAsync(string category, Guid id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products
            .GetByIdAndCategoryAsync(
                product => product.ProductCategory.CategoryName == category && product.Id == id,
                cancellationToken);

        return product;
    }
}