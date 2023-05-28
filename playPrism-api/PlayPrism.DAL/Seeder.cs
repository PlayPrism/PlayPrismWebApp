using Bogus;
using Microsoft.Extensions.Logging;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Enums;
using PlayPrism.DAL.Abstractions.Interfaces;
using System.Runtime.CompilerServices;

namespace PlayPrism.DAL;

/// <inheritdoc />
public class Seeder : ISeeder
{
    private readonly ILogger<Seeder> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// <see cref="ISeeder"/>
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="unitOfWork"><see cref="IUnitOfWork"/></param>
    public Seeder(ILogger<Seeder> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task SeedIfNeededAsync()
    {
        var categoriesIsExist = await _unitOfWork.Categories.ExistAsync(null);

        _logger.LogInformation("Seeder: Checking if seed is needed");
        if (!categoriesIsExist)
        {
            await GenerateProductCategoriesAsync();
            await GenerateProductsAsync();
            await GenerateProductConfigurationsAsync();
            await GenerateProductVariationsAsync();
            await GenerateUsersAsync();
            await GenerateGiveawaysAsync();
            await GenerateProductItemsAsync();
            await GeneratePaymentMethodsAsync();
            await GenerateOrdersAsync();
            await GenerateOrderItemsAsync();
            await UpdateProductItemsAsync();
        }
        else
        {
            _logger.LogInformation("Seeder: Database is already seeded or not empty");
        }
    }

    private async Task GenerateProductsAsync()
    {
        var gamesCategoryId = (await _unitOfWork.Categories.GetByConditionAsync(x => x.CategoryName == "Games")).First().Id;

        var games = new[]
        {
            "Super Mario Odyssey",
            "The Legend of Zelda: Breath of the Wild",
            "Super Smash Bros. Ultimate",
            "Animal Crossing: New Horizons",
            "Minecraft",
            "Pokémon Sword and Shield",
            "Mario Kart 8 Deluxe",
        };

        var productGames = games.Select(game =>
                new Faker<Product>()
                    .RuleFor(p => p.Name, game)
                    .RuleFor(p => p.HeaderImage, f => f.Image.PicsumUrl())
                    .RuleFor(p => p.Images, f => Enumerable.Range(1, 3)
                        .Select(_ => f.Image.PicsumUrl())
                        .ToArray())
                    .RuleFor(p => p.ShortDescription, f => f.Lorem.Sentence())
                    .RuleFor(p => p.DetailedDescription, f => f.Lorem.Paragraph())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(2.99M, 9.99M))
                    .RuleFor(p => p.ReleaseDate, f => f.Date.Future().ToUniversalTime())
                    .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
                    .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                    .RuleFor(p => p.ProductCategoryId, gamesCategoryId)
                    .Generate())
            .ToList();

        await _unitOfWork.Products.AddManyAsync(productGames);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Product (games) created");
    }

    private async Task GenerateProductCategoriesAsync()
    {
        var categories = new[] { "Games", "Software", "Accounts" };

        var productCategories = categories.Select(category =>
                new Faker<ProductCategory>()
                .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
                .RuleFor(p => p.CategoryName, category)
                .Generate())
            .ToList();

        await _unitOfWork.Categories.AddManyAsync(productCategories);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Product categories created");
    }

    private async Task GenerateProductConfigurationsAsync()
    {
        var configurations = new[]
        {
            "Genre", "Platform"
        };

        var gamesCategoryId = (await _unitOfWork.Categories.GetByConditionAsync(x => x.CategoryName == "Games")).First().Id;

        var productConfigurations = configurations.Select(configuration =>
                new Faker<ProductConfiguration>()
                .RuleFor(p => p.ConfigurationName, configuration)
                .RuleFor(p => p.CategoryId, gamesCategoryId)
                .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
                .Generate())
            .ToList();

        await _unitOfWork.ProductConfigurations.AddManyAsync(productConfigurations);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Product configurations created");
    }
    private async Task GenerateProductVariationsAsync()
    {
        var genres = new[]
        {
            "Action", "Fighting", "Horror", "Sport", "Simulator", "Adventure"
        };

        var products = await _unitOfWork.Products.GetByConditionAsync(x => x.ProductCategory.CategoryName == "Games");
        var productConfigurationGenreId = (await _unitOfWork.ProductConfigurations.GetByConditionAsync(x => x.ConfigurationName == "Genre")).First().Id;

        var productVariations = new List<VariationOption>();
        foreach (var product in products)
        {
            var newProdVariationOption = new Faker<VariationOption>()
                .RuleFor(p => p.ProductId, product.Id)
                .RuleFor(p => p.Value, f => f.PickRandom(genres))
                .RuleFor(p => p.ProductConfigurationId, productConfigurationGenreId)
                .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
                .Generate();

            productVariations.Add(newProdVariationOption);
        }



        await _unitOfWork.Variations.AddManyAsync(productVariations);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Product variations created");
    }
    
    private async Task GenerateUsersAsync()
    {
        var games = await _unitOfWork.Products.GetByConditionAsync(x => x.ProductCategory.CategoryName == "Games");
        var userProfiles = games.Select(user =>
                   new Faker<UserProfile>()
                   .RuleFor(p => p.Nickname, f => f.Internet.UserName())
                   .RuleFor(p => p.Email, f => f.Internet.Email())
                   .RuleFor(p => p.Password, f => f.Internet.Password())
                   .RuleFor(p => p.Role, Role.User)
                   .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                   .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
                   .Generate())
            .ToList();
        await _unitOfWork.Users.AddManyAsync(userProfiles);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: User profiles created");
    }

    private async Task GenerateGiveawaysAsync()
    {
        var games = await _unitOfWork.Products.GetByConditionAsync(x => x.ProductCategory.CategoryName == "Games");
        var users = await _unitOfWork.Users.GetAllAsync();

        var giveaways = games.Select(game =>
            new Faker<Giveaway>()
            .RuleFor(p => p.ProductId, game.Id)
            .RuleFor(p => p.WinnerId, f => f.PickRandom(users).Id)
            .RuleFor(p => p.DateCreated, DateTime.UtcNow)
            .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
            .RuleFor(p => p.ExpirationDate, f => f.Date.Future().ToUniversalTime())
            .Generate())
        .ToList();

        await _unitOfWork.Giveaways.AddManyAsync(giveaways);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Giveaways created");
    }

    private async Task GenerateProductItemsAsync() 
    {
        var games = await _unitOfWork.Products.GetByConditionAsync(x => x.ProductCategory.CategoryName == "Games");
        var productItems = games.Select(game =>
                   new Faker<ProductItem>()
                   .RuleFor(p => p.ProductId, game.Id)
                   .RuleFor(p => p.Value, f => f.Random.AlphaNumeric(5).ToUpper() + "-" + f.Random.AlphaNumeric(5).ToUpper() + "-" + f.Random.AlphaNumeric(5).ToUpper())
                   .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                   .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
                   .Generate())
        .ToList();

        await _unitOfWork.ProductItems.AddManyAsync(productItems);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Product items created");
    }

    private async Task GeneratePaymentMethodsAsync() 
    {
        var paymentMethods = new List<PaymentMethod>();
        var payment = new Faker<PaymentMethod>()
                .RuleFor(p => p.Name, f => f.Company.CatchPhrase())
                .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                .RuleFor(p => p.DateUpdated, DateTime.UtcNow).Generate();
        paymentMethods.Add(payment);

        await _unitOfWork.PaymentMethods.AddManyAsync(paymentMethods);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Payment methods created");
    }

    private async Task GenerateOrdersAsync() 
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        var payments = await _unitOfWork.PaymentMethods.GetAllAsync();
        var orders = users.Select(user =>
            new Faker<Order>()
            .RuleFor(p => p.UserId, user.Id)
            .RuleFor(p => p.OrderTotal, f => f.Random.Number(5))
            .RuleFor(p => p.PaymentMethodId, f => f.PickRandom(payments).Id)
            .RuleFor(p => p.DateCreated, DateTime.UtcNow)
            .RuleFor(p => p.DateUpdated, DateTime.UtcNow).Generate()).ToList();

        await _unitOfWork.Orders.AddManyAsync(orders);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Orders created");
    }

    private async Task GenerateOrderItemsAsync() 
    {
        var orders = await _unitOfWork.Orders.GetAllAsync();
        var productItems = await _unitOfWork.ProductItems.GetAllAsync();
        var orderItems = productItems.Select(productItem =>
            new Faker<OrderItem>()
            .RuleFor(p => p.OrderId, f => f.PickRandom(orders).Id)
            .RuleFor(p => p.ProductItemId, productItem.Id)
            .RuleFor(p => p.DateCreated, DateTime.UtcNow)
            .RuleFor(p => p.DateUpdated, DateTime.UtcNow).Generate()).ToList();

        await _unitOfWork.OrderItems.AddManyAsync(orderItems);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Order items created");
    }

    private async Task UpdateProductItemsAsync() 
    {
        var productItems = (await _unitOfWork.ProductItems.GetAllAsync()).ToList();
        var orderItems = (await _unitOfWork.OrderItems.GetAllAsync()).ToList();
        for (int i = 0; i < productItems.Count(); i++)
        {
            productItems[i].OrderItemId = new Faker().PickRandom(orderItems[i]).Id;
            await _unitOfWork.ProductItems.Update(productItems[i]);
        }

        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Product items updated with order item id");
    }
}