using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Funcionarios.Models;

namespace Restaurante.Infra.Common.Persistence.Interfaces
{
    public interface IRestauranteDbContext : IDbContext
    {
        DbSet<Funcionario> Funcionarios { get; }
        DbSet<Entregador> Entregadores { get; }
        DbSet<Bank> Banks { get; }
    }
}
