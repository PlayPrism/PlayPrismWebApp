using System.Linq.Expressions;
using PlayPrism.Core.Domain;

namespace PlayPrism.BLL.Constants;

/// <summary>
/// Represents selectors for emtities
/// </summary>
public static class EntitiesSelectors
{
    /// <summary>
    /// Gets or sets product selector
    /// </summary>
    public static Expression<Func<Product, Product>> ProductSelector => q => new Product
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

    /// <summary>
    /// Gets or sets Refresh token selector selector
    /// </summary>
    public static Expression<Func<RefreshToken, RefreshToken>> RefreshTokenSelector => q => new RefreshToken
    {
        Id = q.Id,
        Token = q.Token,
        User = q.User,
        ExpireDate = q.ExpireDate
    };
}