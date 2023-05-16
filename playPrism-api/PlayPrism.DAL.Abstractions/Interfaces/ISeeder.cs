namespace PlayPrism.DAL.Abstractions.Interfaces;

/// <summary>
/// Represents database seeder.
/// </summary>
public interface ISeeder
{
    /// <summary>
    /// Seed database with information if needed.
    /// </summary>
    /// <returns>A filled database with fake information.</returns>
    Task SeedIfNeededAsync();
}