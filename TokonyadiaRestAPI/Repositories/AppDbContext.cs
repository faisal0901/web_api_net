using Microsoft.EntityFrameworkCore;
using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseDetail> PurchaseDetails => Set<PurchaseDetail>();
    public DbSet<UserCredential> UserCredentials => Set<UserCredential>();
    public DbSet<Role> Roles => Set<Role>();
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
    
            entity.HasIndex(customer => customer.PhoneNumber).IsUnique();
        });
        modelBuilder.Entity<Store>(e =>
        {
            e.HasIndex(s => s.PhoneNumber).IsUnique();
            e.HasIndex(s => s.SiupNumber).IsUnique();
        });
        modelBuilder.Entity<UserCredential>(e =>
        {
            e.HasIndex(s => s.Email).IsUnique();
         
        });
    }
}