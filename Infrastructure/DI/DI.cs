
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using Infrastructure.Services;
namespace Infrastructure.DI
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=.;Database=HaiPhongTech;Trusted_Connection=True;TrustServerCertificate=True")
            ); // hoặc UseNpgsql, UseSqlite...

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
