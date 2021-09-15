using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using MediatR;
using System.Reflection;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Application.Common.Services;
using Restaurante.Domain.Users.Common.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Restaurante.Domain.Users.Common.Services.Interfaces;
using Restaurante.Application.Users.Common.Services;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Application.Users.Deliveries.Services;

[assembly: InternalsVisibleTo("Restaurante.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Restaurante.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddNotifier()
                .AddServices()
                .AddMappers()
                .AddIntegrationServices(configuration)
                .AddTokenService(configuration);

        internal static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                                    .AssignableTo(typeof(IEntityService<>)))
                                    .AsMatchingInterface()
                                    .AsImplementedInterfaces()
                                    .WithTransientLifetime());

        internal static IServiceCollection AddNotifier(this IServiceCollection services) =>
            services.AddScoped<INotifier, Notifier>();

        internal static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(
                                configuration
                                    .GetSection(nameof(IntegrationConfiguration))
                                    .Get<IntegrationConfiguration>())
                .AddTransient<IEntregadorIntegrationService, DeliveriesIntegrationService>();

        internal static IServiceCollection AddMappers(this IServiceCollection services) =>
             services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                                    .AssignableTo(typeof(IMapper<,>)))
                                    .AsMatchingInterface()
                                    .AsImplementedInterfaces()
                                    .WithTransientLifetime());

        internal static IServiceCollection AddTokenService(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfiguration = configuration.GetSection(nameof(TokenConfiguration)).Get<TokenConfiguration>();

            services.AddSingleton(tokenConfiguration);

            var key = Encoding.UTF8.GetBytes(tokenConfiguration.Secret);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
