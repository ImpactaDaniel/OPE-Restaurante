using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Infra.Common;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Common.Settings;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Restaurante.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Restaurante.Infra
{
    public static class InfraConfiguration
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddContext(configuration)
                .AddRepositories()
                .AddMessageServices(configuration);

        internal static IServiceCollection AddMessageServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(configuration
                    .GetSection(nameof(SmtpEmailSettings))
                    .Get<SmtpEmailSettings>())
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                                    .AssignableTo<IMessageSenderService>())
                                    .AsImplementedInterfaces()
                                    .WithTransientLifetime());

        internal static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                                    .AssignableTo(typeof(IDomainRepository<>)))
                                    .AsImplementedInterfaces()
                                    .WithTransientLifetime());

        internal static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<RestauranteDbContext>(options => options
                                                        .UseSqlite(
                                                                configuration.GetConnectionString("Default"),
                                                                sqlServer => sqlServer
                                                                    .MigrationsAssembly(typeof(RestauranteDbContext).Assembly.FullName)))
                .AddTransient<IRestauranteDbContext, RestauranteDbContext>(provider => provider.GetService<RestauranteDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();
    }
}
