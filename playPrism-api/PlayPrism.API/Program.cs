using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlayPrism.API;
using PlayPrism.API.Extensions;
using PlayPrism.DAL.Abstractions.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
    containerBuilder => containerBuilder.RegisterModule(new ApiDiModule(builder.Configuration)));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

builder.Services.AddAutoMapper(typeof(PlayPrism.Contracts.Mappings.ProductProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromSeconds(10),
        };
    });

builder.Services.AddAuthorization();

builder.Host.UseSerilog();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Configure Serilog
builder.Services.AddLogging(loggingBuilder =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
    loggingBuilder.AddSerilog(dispose: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseRouting();

app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

// Database initialization
await app.MigrateDatabaseAsync();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
    await seeder.SeedIfNeededAsync();
}

app.Run();

Log.CloseAndFlush();