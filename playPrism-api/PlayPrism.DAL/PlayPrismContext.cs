using Microsoft.EntityFrameworkCore;
using PlayPrism.Core.Domain;

namespace PlayPrism.DAL;

/// <inheritdoc />
public class PlayPrismContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayPrismContext"/> class.
    /// </summary>
    /// <param name="options">Receives database context options that represents connection string.</param>
    public PlayPrismContext(DbContextOptions<PlayPrismContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets representation Giveaways table in database.
    ///</summary>    
    public DbSet<Giveaway> Giveaways { get; set; }

    /// <summary>
    /// Gets or sets representation Orders table in database.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets representation OrderItems table in database.
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; }

    /// <summary>
    /// Gets or sets representation PaymentMethods table in database.
    /// </summary>
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    /// <summary>
    /// Gets or sets representation Products table in database.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets representation ProductCategories table in database.
    /// </summary>
    public DbSet<ProductCategory> ProductCategories { get; set; }

    /// <summary>
    /// Gets or sets representation ProductConfigurations table in database.
    /// </summary>
    public DbSet<ProductConfiguration> ProductConfigurations { get; set; }

    /// <summary>
    /// Gets or sets representation ProductItems table in database.
    /// </summary>
    public DbSet<ProductItem> ProductItems { get; set; }

    /// <summary>
    /// Gets or sets representation UserProfiles table in database.
    /// </summary>
    public DbSet<UserProfile> UserProfiles { get; set; }

    /// <summary>
    /// Gets or sets representation UserReviews table in database.
    /// </summary>
    public DbSet<UserReview> UserReviews { get; set; }

    /// <summary>
    /// Gets or sets representation VariationOptions table in database.
    /// </summary>
    public DbSet<VariationOption> VariationOptions { get; set; }
    
    /// <summary>
    /// Gets or sets representation RefreshToken table in database.
    /// </summary>
    public DbSet<RefreshToken> RefreshToken { get; set; }
    
    

    /// <inheritdoc />
    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((BaseEntity) entityEntry.Entity).DateUpdated = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity) entityEntry.Entity).DateCreated = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>().Property(e => e.Role).HasConversion<string>();
        
        modelBuilder.Entity<Giveaway>()
            .HasMany(bc => bc.Participants)
            .WithMany(c => c.Giveaways);

        modelBuilder.Entity<Giveaway>()
            .HasOne(bc => bc.Winner)
            .WithMany(b => b.WonGiveaways)
            .HasForeignKey(bc => bc.WinnerId);

        modelBuilder.Entity<Order>()
            .HasOne(bc => bc.PaymentMethod)
            .WithMany(b => b.Orders)
            .HasForeignKey(bc => bc.PaymentMethodId);

        modelBuilder.Entity<Order>()
            .HasOne(bc => bc.UserProfile)
            .WithMany(c => c.Orders)
            .HasForeignKey(bc => bc.UserId);

        modelBuilder.Entity<Order>()
            .HasMany(bc => bc.OrderItems)
            .WithOne(c => c.Order)
            .HasForeignKey(bc => bc.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(a => a.ProductItem)
            .WithOne(b => b.OrderItem)
            .HasForeignKey<ProductItem>(b => b.OrderItemId);

        modelBuilder.Entity<Product>()
            .HasMany(bc => bc.ProductItems)
            .WithOne(c => c.Product)
            .HasForeignKey(bc => bc.ProductId);

        modelBuilder.Entity<Product>()
            .HasMany(bc => bc.UserReviews)
            .WithOne(c => c.Product)
            .HasForeignKey(bc => bc.ProductId);

        modelBuilder.Entity<Product>()
            .HasMany(bc => bc.VariationOptions)
            .WithOne(option => option.Product)
            .HasForeignKey(bc => bc.ProductId);
        
        modelBuilder.Entity<ProductConfiguration>()
            .HasOne(x => x.Category)
            .WithMany(option => option.ProductConfigurations)
            .HasForeignKey(bc => bc.CategoryId);

        modelBuilder.Entity<VariationOption>()
            .HasOne(bc => bc.ProductConfiguration)
            .WithMany(configuration => configuration.VariationOptions)
            .HasForeignKey(option => option.ProductConfigurationId);

        modelBuilder.Entity<VariationOption>()
            .HasOne(options => options.Product)
            .WithMany(configuration => configuration.VariationOptions)
            .HasForeignKey(option => option.ProductId);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(opt => opt.User)
            .WithOne(x => x.RefreshToken)
            .HasForeignKey<RefreshToken>(token => token.UserId);

    }
}