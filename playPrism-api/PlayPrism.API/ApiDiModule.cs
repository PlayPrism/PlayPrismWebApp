using Autofac;
using PlayPrism.BLL;

namespace PlayPrism.API;

/// <inheritdoc />
public class ApiDiModule : Module
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiDiModule"/> class.
    /// </summary>
    /// <param name="configuration">IConfiguration that reads config from appsettings.json.</param>
    public ApiDiModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule(new ServiceDiModule(_configuration));
    }
}