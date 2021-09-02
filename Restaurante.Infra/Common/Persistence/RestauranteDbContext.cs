using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Infra.Common.Persistence.Interfaces;

namespace Restaurante.Infra.Common.Persistence
{
    internal class RestauranteDbContext : DbContext, IRestauranteDbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options)
        {
        }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
