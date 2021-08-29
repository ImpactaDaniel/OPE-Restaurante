using Microsoft.EntityFrameworkCore;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;

namespace Restaurante.Infra.Users.Funcionarios
{
    internal class FuncionariosRepository :
        DataRepository<IRestauranteDbContext, Funcionario>,
        IFuncionarioDomainRepository<Funcionario>
    {
        public FuncionariosRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<Funcionario> CreateFuncionario(Funcionario funcionario, Funcionario usuario, CancellationToken cancellationToken = default)
        {
            await Data.Funcionarios.AddAsync(funcionario, cancellationToken);
            await Data.SaveChangesAsync(cancellationToken);
            return funcionario;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entity = await
                All()
                .FirstAsync(e => e.Id == id, cancellationToken);

            Data.Funcionarios.Remove(entity);
            await Data.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Funcionario> Get(int id, CancellationToken cancellationToken = default)
        {
                var entity = await
                    All()
                    .FirstAsync(e => e.Id == id, cancellationToken);

                return entity;
        }

        public Task<Funcionario> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
