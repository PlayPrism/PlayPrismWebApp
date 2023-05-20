using Autofac;
using PlayPrism.BLL;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Services;
using PlayPrism.Core.Settings;
using Stripe;

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
        // Stripe settings
        var stripeSettings = _configuration.GetSection(nameof(StripeSettings)).Get<StripeSettings>();
        StripeConfiguration.ApiKey = stripeSettings.SecretKey;
        builder.RegisterType<CustomerService>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<ChargeService>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<TokenService>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<StripeService>().As<IStripeService>().InstancePerLifetimeScope();

        builder.RegisterModule(new ServiceDiModule(_configuration));
    }
}