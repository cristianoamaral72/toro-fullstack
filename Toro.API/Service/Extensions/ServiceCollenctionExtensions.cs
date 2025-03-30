using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toro.Data.DbContext;
using Toro.Data.Repository;
using Toro.Domain.Interfaces.Repository;
using Toro.Domain.Interfaces.Service;
using Toro.Service.Services;

namespace Toro.Service.Extensions;

public static class ServiceCollenctionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do DbContext com a connection string definida no appsettings.json.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IOrderProcessingService, OrderProcessingService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        return services;
    }
}