using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleStock.Domain.Models;

namespace SimpleStock.Infrastructure.DataContexts;

public class SimpleStockDataContext : DbContext
{
    public SimpleStockDataContext(DbContextOptions<SimpleStockDataContext> options )
        : base( options ) { }

    public DbSet<AddressModel> Adressess { get; set; }
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<OrderItemModel> SaleItems { get; set; }
    public DbSet<OrderModel> Sales { get; set; }

    public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is EntityModel && (
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted));

        foreach (var entry in entries)
        {
            DateTimeOffset dateTime = DateTimeOffset.Now;
            switch (entry.State)
            {
                case EntityState.Added:
                    ((EntityModel)entry.Entity).CreatedAt = dateTime;
                    ((EntityModel)entry.Entity).IsDeleted = false;
                    break;
                case EntityState.Modified:
                    ((EntityModel)entry.Entity).UpdatedAt = dateTime;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    ((EntityModel)entry.Entity).IsDeleted = true;
                    break;
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
