using Microsoft.EntityFrameworkCore;
using Toro.Domain.Entities;

namespace Toro.Data.DbContext;

public class ToroContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ToroContext(DbContextOptions<ToroContext> options) : base(options) { }

    // DbSets para as entidades
    public DbSet<Product> Products { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da entidade Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductId); // Chave primária
            entity.Property(p => p.BondAsset).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Index).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Tax).HasColumnType("decimal(5,2)");
            entity.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(p => p.Stock).IsRequired();
        });

        // Configuração da entidade Account
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.AccountId); // Chave primária
            entity.Property(a => a.ClientId).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Balance).HasColumnType("decimal(18,2)");
        });

        // Configuração da entidade Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId); // Chave primária
            entity.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");
            entity.HasOne(o => o.Account)
                  .WithMany()
                  .HasForeignKey(o => o.AccountId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.Product)
                  .WithMany()
                  .HasForeignKey(o => o.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}