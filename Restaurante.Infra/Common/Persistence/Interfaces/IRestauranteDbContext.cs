using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Employees.Models;

namespace Restaurante.Infra.Common.Persistence.Interfaces
{
    public interface IRestauranteDbContext : IDbContext
    {
        DbSet<Employee> Employees { get; }
    }
}
