using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlayPrism.DAL.Abstractions.Interfaces;
using PlayPrism.DAL.Repository;

namespace PlayPrism.DAL;

/// <inheritdoc />
public class RepositoryDiModule : Module
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryDiModule"/> class.
    /// </summary>
    /// <param name="configuration">IConfiguration that reads config from appsettings.json.</param>
    public RepositoryDiModule(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PlayPrismContext>().WithParameter(
            (info, context) => info.ParameterType == typeof(DbContextOptions<PlayPrismContext>),
            (info, context) => new DbContextOptionsBuilder<PlayPrismContext>()
                .UseNpgsql(this._configuration.GetConnectionString("DbConnection"))
                .Options);
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}