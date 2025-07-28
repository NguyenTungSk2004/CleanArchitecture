namespace Infrastructure.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using Infrastructure.Services;
using Domain.Repositories;
using Infrastructure.Repositories.Products;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString)); // hoặc UseNpgsql, UseSqlite...

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}
