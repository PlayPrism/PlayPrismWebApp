using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Constants;
using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Domain.Filters;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

using System.Linq.Expressions;

/// <inheritdoc />
public class ProductsService : IProductsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsService"/> class.
    /// </summary>
    /// <param name="unitOfWork"><see cref="IUnitOfWork"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IList<ProductResponse>> GetProductsByFiltersWithPaginationAsync(
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

            var products = await _unitOfWork.Products
                .GetPageWithMultiplePredicatesAsync(predicates, pageInfo, EntitiesSelectors.ProductSelector, cancellationToken);

            var result = _mapper.Map<List<ProductResponse>>(products);
            return result;
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

        var result = _mapper.Map<IEnumerable<CategoryFiltersResponse>>(categoryConfigurations);
        return result;
    }

    /// <inheritdoc />
    public async Task<ProductResponse> GetProductByIdAsync(string category, Guid id, CancellationToken cancellationToken)
    {
        var product = (await _unitOfWork.Products
            .GetByConditionAsync(
                product => product.ProductCategory.CategoryName == category && product.Id == id,
                EntitiesSelectors.ProductSelector,
                cancellationToken)).FirstOrDefault();
        
        var result = _mapper.Map<ProductResponse>(product);
        return result;
    }
}