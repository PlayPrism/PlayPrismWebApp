using Autofac;
using Microsoft.Extensions.Configuration;
using PlayPrism.DAL;

namespace PlayPrism.BLL;

/// <inheritdoc />
public class ServiceDiModule : Module
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceDiModule"/> class.
    /// </summary>
    /// <param name="configuration">IConfiguration that reads config from appsettings.json.</param>
    public ServiceDiModule(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule(new RepositoryDiModule(this._configuration));
    }
}