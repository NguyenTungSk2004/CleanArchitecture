using Application.Services.BaseServices.HardDelete;
using Application.Services.BaseServices.Recovery;
using Application.Services.BaseServices.SoftDelete;
using SharedKernel.Base;
using SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
   public static class GenericHandlerRegistration
    {
        public static IServiceCollection AddAllGenericHandlers(this IServiceCollection services)
        {
            var baseEntityType = typeof(EntityBase);
            var aggregateRootType = typeof(IAggregateRoot);
            var auditableType = typeof(IAuditable);

            var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && a.FullName != null && !a.FullName.StartsWith("Microsoft"))
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                });

            var entities = allTypes
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.IsPublic &&
                    baseEntityType.IsAssignableFrom(t) &&
                    aggregateRootType.IsAssignableFrom(t) &&
                    auditableType.IsAssignableFrom(t)
                );

            foreach (var entityType in entities)
            {
                Register(services, entityType, typeof(SoftDeleteCommand<>), typeof(SoftDeleteHandler<>));
                Register(services, entityType, typeof(HardDeleteCommand<>), typeof(HardDeleteHandler<>));
                Register(services, entityType, typeof(RecoveryCommand<>), typeof(RecoveryHandler<>));
            }

            return services;
        }

        private static void Register(IServiceCollection services, Type entityType, Type commandOpen, Type handlerOpen)
        {
            var commandType = commandOpen.MakeGenericType(entityType);
            var handlerType = handlerOpen.MakeGenericType(entityType);
            var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(commandType, typeof(bool));

            services.AddTransient(interfaceType, handlerType);
        }
    }

}