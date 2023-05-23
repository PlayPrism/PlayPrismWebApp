﻿using Microsoft.EntityFrameworkCore;
using PlayPrism.DAL;

namespace PlayPrism.API.Extensions;

/// <summary>
/// Represents a migration manager that checks whether the database has been created or not
/// </summary>
public static class MigrationManager
{
    /// <summary>
    /// Migrate the database to the latest version
    /// </summary>
    /// <param name="webApp"><see cref="WebApplication"/></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task MigrateDatabaseAsync(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();

        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(typeof(MigrationManager));

        var appContext = scope.ServiceProvider.GetRequiredService<PlayPrismContext>();
        try
        {
            logger.LogInformation("MigrationManager: Trying to migrate database");
            await appContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "MigrationManager: Database migration failed.");
            throw;
        }

        logger.LogInformation("MigrationManager: The database migration was successful");
    }
}
