using Microsoft.EntityFrameworkCore;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Domain.Users.Entregadores.Repositories;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Funcionarios.Models;

namespace Restaurante.Infra.Users.Entregadores
{
    internal class EntregadoresRepository :
        DataRepository<IRestauranteDbContext, Entregador>,
        IEntregadorDomainRepository
    {
        public EntregadoresRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<Entregador> CreateFuncionario(Entregador entregador, Funcionario usuario, CancellationToken cancellationToken = default)
        {
            var user = await
                Data
                .Funcionarios
                .FirstAsync(f => f.Id == usuario.Id, cancellationToken);
            await Data.Entregadores.AddAsync(entregador, cancellationToken);
            await Data.SaveChangesAsync(cancellationToken);
            return entregador;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entity = await
                All()
                .FirstAsync(e => e.Id == id, cancellationToken);

            Data.Entregadores.Remove(entity);

            await Data.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Entregador> Get(int id, CancellationToken cancellationToken = default)
        {
            var entity = await
                All()
                .FirstAsync(e => e.Id == id, cancellationToken);
            return entity;
        }

        public Task<Veiculo> GetVehicle(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entregador> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
