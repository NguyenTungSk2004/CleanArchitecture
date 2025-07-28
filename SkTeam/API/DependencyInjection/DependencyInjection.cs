using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Đăng ký FluentValidation
        services.AddValidatorsFromAssembly(Assembly.Load("Application"));
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        // MediatR - scan từ Assembly Application
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.Load("Application"));
        });

        return services;
    }
}
