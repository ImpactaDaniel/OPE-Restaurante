using Microsoft.Extensions.DependencyInjection;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Restaurante.Test")]
namespace Restaurante.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) =>
            services
                .AddFactories()
                .AddServices();

        internal static IServiceCollection AddFactories(this IServiceCollection services) =>
            services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                                    .AssignableTo(typeof(IFactory<>)))
                                    .AsMatchingInterface()
                                    .WithTransientLifetime());

        internal static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                                    .AssignableTo(typeof(IEntityService<>)))
                                    .AsMatchingInterface()
                                    .WithTransientLifetime());
    }
}
