using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using MediatR;
using System.Reflection;

[assembly: InternalsVisibleTo("Restaurante.Test")]
namespace Restaurante.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) =>
            services
                .AddMediatR(Assembly.GetExecutingAssembly());
    }
}
