using Autofac;
using Microsoft.Extensions.Configuration;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.BLL.Helpers;
using PlayPrism.BLL.Services;
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
        _configuration = configuration;
    }

    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EmailWorker>().InstancePerLifetimeScope();
        builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        builder.RegisterType<ProductsService>().As<IProductsService>().InstancePerLifetimeScope();
        builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
        builder.RegisterType<GiveawayService>().As<IGiveawaysService>().InstancePerLifetimeScope();
        builder.RegisterModule(new RepositoryDiModule(_configuration));
    }
}