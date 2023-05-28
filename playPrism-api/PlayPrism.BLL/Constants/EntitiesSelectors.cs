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
        Images = q.Images.ToArray(),
        ReleaseDate = q.ReleaseDate,
        Price = q.Price,
    };

    /// <summary>
    /// Gets or sets giveaway selector
    /// </summary>
    public static Expression<Func<Giveaway, Giveaway>> GiveawaySelector => q => new Giveaway
    {
        Id = q.Id,
        Product = new Product
        {
            Id = q.Product.Id,
            Name = q.Product.Name,
            HeaderImage = q.Product.HeaderImage,
            Price = q.Product.Price,
            ReleaseDate = q.Product.ReleaseDate,
            ShortDescription = q.Product.ShortDescription,
            DetailedDescription = q.Product.DetailedDescription,
            ProductCategory = q.Product.ProductCategory,
            ProductCategoryId = q.Product.ProductCategoryId,
            VariationOptions = q.Product.VariationOptions.Select(option => new VariationOption
            {
                Id = option.Id,
                ProductConfiguration = option.ProductConfiguration,
                Value = option.Value,
            }).ToList(),
            ProductItems = q.Product.ProductItems.ToList(),
        },
        Participants = q.Participants.Select(participant => new UserProfile
        {
            Id = participant.Id,
            Nickname = participant.Nickname
        }).ToList(),
        Winner = new UserProfile 
        {
            Id = q.Winner.Id,
            Nickname = q.Winner.Nickname,
            Image = q.Winner.Image,
            Email = q.Winner.Email,
        },
        ExpirationDate = q.ExpirationDate,
    };

    /// <summary>
    /// Gets or sets Refresh token selector selector
    /// </summary>
    public static Expression<Func<RefreshToken, RefreshToken>> RefreshTokenSelector => q => new RefreshToken
    {
        Id = q.Id,
        UserId = q.UserId,
        Token = q.Token,
        User = q.User,
        ExpireDate = q.ExpireDate
    };

    public static Expression<Func<OrderItem, OrderItem>> HistoryItemSelector => q => new OrderItem
    {
        Id = q.Id,
        ProductItem = new ProductItem 
        {
            ProductId = q.ProductItem.ProductId,
            Product = new Product
            {
                Name = q.ProductItem.Product.Name,
                HeaderImage = q.ProductItem.Product.HeaderImage,
                Price = q.ProductItem.Product.Price,
            },
            Value = q.ProductItem.Value,
        },
        Order = new Order 
        {
            UserId = q.Order.UserId,
        },
        DateCreated = q.DateCreated,
    };

    public static Expression<Func<UserProfile, UserProfile>> UserSelector => q => new UserProfile
    {
        Id = q.Id,
        Email = q.Email,
        Nickname = q.Nickname,
        Password = q.Password,
        Image = q.Image,
        Role = q.Role,
        Orders = q.Orders.Select(order => new Order
        {
            Id = order.Id,
            OrderTotal = order.OrderTotal,
            PaymentMethodId = order.PaymentMethodId,
        }).ToList(),
        Giveaways = q.Giveaways.Select(giveaways => new Giveaway
        {
            Id = giveaways.Id,
        }).ToList(),
        WonGiveaways = q.WonGiveaways.Select(w => new Giveaway
        {
            Id = w.Id,
        }).ToList(),
    };
}