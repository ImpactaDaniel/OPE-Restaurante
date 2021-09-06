using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Funcionarios.Models;

namespace Restaurante.Infra.Common.Persistence.Interfaces
{
    public interface IRestauranteDbContext : IDbContext
    {
        DbSet<Funcionario> Funcionarios { get; }
    }
}
