using Microsoft.EntityFrameworkCore;
using Toro.Domain.Entities;

namespace Toro.Data.DbContext;

public class ApplicationDbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Account?> Accounts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasData(
                new Product
                {
                    BondAsset = "CDB",
                    Index = "IPCA",
                    Tax = 5.0m,
                    IssuerName = "Banco Teste",
                    UnitPrice = 1000,
                    Stock = 100
                },
                new Product
                {
                    BondAsset = "LCI",
                    Index = "Pre",
                    Tax = 12.0m,
                    IssuerName = "Banco Teste 2",
                    UnitPrice = 2000,
                    Stock = 20
                });
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasData(
                new Account
                {
                    ClientId = "12454",
                    Balance = 1000.00m
                });
        });
    }
}