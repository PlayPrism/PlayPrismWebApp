using Bogus;
using Microsoft.Extensions.Logging;
using PlayPrism.Core.Domain;
using PlayPrism.DAL.Abstractions.Interfaces;

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
                    .RuleFor(p => p.HeaderImage, f => f.Image.PlaceImgUrl())
                    .RuleFor(p => p.ShortDescription, f => f.Lorem.Sentence())
                    .RuleFor(p => p.DetailedDescription, f => f.Lorem.Paragraph())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(2.99M, 9.99M))
                    .RuleFor(p => p.ReleaseDate,  f => f.Date.Future().ToUniversalTime())
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
        _logger.LogInformation("Seeder: Product categories created");
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
                .RuleFor(p => p.ProductConfigurationId,  productConfigurationGenreId)
                .RuleFor(p => p.DateCreated, DateTime.UtcNow)
                .RuleFor(p => p.DateUpdated, DateTime.UtcNow)
                .Generate();

            productVariations.Add(newProdVariationOption);
        }

        
        
        await _unitOfWork.Variations.AddManyAsync(productVariations);
        await _unitOfWork.SaveAsync();
        _logger.LogInformation("Seeder: Product categories created");
    }
}