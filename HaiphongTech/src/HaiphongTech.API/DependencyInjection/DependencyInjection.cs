using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace HaiphongTech.API.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Đăng ký FluentValidation
        services.AddValidatorsFromAssembly(Assembly.Load("HaiphongTech.Application"));
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        // MediatR - scan từ Assembly Application
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.Load("HaiphongTech.Application"));
        });

        return services;
    }
}
