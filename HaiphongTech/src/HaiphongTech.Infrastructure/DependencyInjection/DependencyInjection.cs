namespace HaiphongTech.Infrastructure.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using HaiphongTech.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HaiphongTech.SharedKernel.Interfaces;
using HaiphongTech.Infrastructure.Services;
using HaiphongTech.Domain.Repositories;
using HaiphongTech.Infrastructure.Repositories.Products;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString)); // hoặc UseNpgsql, UseSqlite...
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}
