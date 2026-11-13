using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Infrastructure.DbContext;

public class ContextDb : Microsoft.EntityFrameworkCore.DbContext
{
    public ContextDb(DbContextOptions<ContextDb> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Franchise> Franchises { get; set; }
    public DbSet<FranchiseClient> FranchiseClients { get; set; }
    public DbSet<FranchiseProduct> FranchiseProducts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Basket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FranchiseProduct>()
            .HasIndex(fp => new { fp.FranchiseId, fp.ProductId })
            .IsUnique();

        modelBuilder.Entity<Review>()
            .HasIndex(r => new { r.ProductId, r.UserId })
            .IsUnique();

        modelBuilder.Entity<FranchiseClient>()
            .HasIndex(fc => new { fc.FranchiseId, fc.UserId })
            .IsUnique();
    }
}