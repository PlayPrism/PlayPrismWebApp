using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;
using PlayPrism.DAL;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

using System.Linq.Expressions;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PlayPrismContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="unitOfWork">Unit of work di.</param>
    public ProductService(IUnitOfWork unitOfWork, PlayPrismContext context)
    {
        this._unitOfWork = unitOfWork;
        this._context = context;
    }

    public async Task<IList<Product>> GetProductsWithFilterAsync(IEnumerable<Filter> filters)
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

        var page = new PageInfo()
        {
            Number = 1,
            Size = 20,
        };
        var tokenSource = new CancellationTokenSource();

        try
        {
            var token = tokenSource.Token;
            tokenSource.CancelAfter(8000);
            //await this._unitOfWork.BeginTransactionAsync();
            var res = await this._unitOfWork.Products.GetPageWithMultiplePredicatesAsync(predicates, page, selector,
                token);
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            tokenSource.Dispose();
        }

        //var res = await this._unitOfWork.Products.GetPageWithMultiplePredicatesAsync(predicates, page, selector);
    }
}