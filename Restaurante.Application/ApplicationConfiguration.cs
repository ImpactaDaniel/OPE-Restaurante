using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using MediatR;
using System.Reflection;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Application.Common.Services;

[assembly: InternalsVisibleTo("Restaurante.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Restaurante.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) =>
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddNotifier()
                .AddServices();

        internal static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                                    .AssignableTo(typeof(IEntityService<>)))
                                    .AsMatchingInterface()
                                    .WithTransientLifetime());

        internal static IServiceCollection AddNotifier(this IServiceCollection services) =>
            services.AddScoped<INotifier, Notifier>();
    }
}
