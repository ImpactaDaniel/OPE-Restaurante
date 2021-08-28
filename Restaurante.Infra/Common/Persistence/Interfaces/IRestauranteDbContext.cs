using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Entregadores;
using Restaurante.Domain.Users.Funcionarios;

namespace Restaurante.Infra.Common.Persistence.Interfaces
{
    public interface IRestauranteDbContext : IDbContext
    {
        DbSet<Funcionario> Funcionarios { get; }
        DbSet<Entregador> Entregadores { get; }
    }
}
