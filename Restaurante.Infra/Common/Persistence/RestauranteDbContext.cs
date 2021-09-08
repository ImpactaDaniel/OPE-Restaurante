using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Infra.Common.Persistence.Interfaces;

namespace Restaurante.Infra.Common.Persistence
{
    internal class RestauranteDbContext : DbContext, IRestauranteDbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options)
        {            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
