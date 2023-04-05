using Microsoft.EntityFrameworkCore;
using PlayPrism.Core.Domain;

namespace PlayPrism.DAL;

public class PlayPrismContext : DbContext
{
    public PlayPrismContext(DbContextOptions<PlayPrismContext> options) : base(options)
    {
    }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    public DbSet<ProductConfiguration> ProductConfigurations { get; set; }
    
    public DbSet<ProductItem> ProductItems { get; set; }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    public DbSet<UserReview> UserReviews { get; set; }
    
    public DbSet<VariationOption> VariationOptions { get; set; }
    
    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).DateUpdated = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).DateCreated = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            .HasForeignKey<ProductItem>(b => b.Id);
        
        modelBuilder.Entity<Product>()
            .HasMany(bc => bc.ProductItems)
            .WithOne(c => c.Product)
            .HasForeignKey(bc => bc.ProductId);
        modelBuilder.Entity<Product>()
            .HasMany(bc => bc.ProductCategories)
            .WithOne(c => c.Product)
            .HasForeignKey(bc => bc.ProductId);
        modelBuilder.Entity<Product>()
            .HasMany(bc => bc.UserReviews)
            .WithOne(c => c.Product)
            .HasForeignKey(bc => bc.ProductId);
        modelBuilder.Entity<Product>()
            .HasMany(bc => bc.ProductConfigurations)
            .WithOne(c => c.Product)
            .HasForeignKey(bc => bc.ProductId);
        
        modelBuilder.Entity<VariationOption>()
            .HasMany(bc => bc.ProductConfigurations)
            .WithOne(c => c.VariationOption)
            .HasForeignKey(bc => bc.VariationOptionId);
    }
}