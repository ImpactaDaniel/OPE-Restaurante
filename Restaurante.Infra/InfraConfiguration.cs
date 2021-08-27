using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Infra.Common.Settings;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Restaurante.Test")]
namespace Restaurante.Infra
{
    public static class InfraConfiguration
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMessageServices(configuration);

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
    }
}
