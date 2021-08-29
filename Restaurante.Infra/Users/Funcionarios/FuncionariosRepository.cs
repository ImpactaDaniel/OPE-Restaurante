using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios;
using Restaurante.Domain.Users.Repositories.Interfaces;
using Restaurante.Infra.Common.Helper;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Domain.Users.Enums;

namespace Restaurante.Infra.Users.Funcionarios
{
    internal class FuncionariosRepository :
        DataRepository<IRestauranteDbContext, Funcionario>,
        IFuncionarioDomainRepository<Funcionario>
    {
        public FuncionariosRepository(IRestauranteDbContext db, INotifier notifier) : base(db, notifier)
        {
        }

        public async Task<Funcionario> CreateFuncionario(Funcionario funcionario, Funcionario usuario, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await
                    Data
                    .Funcionarios
                    .FirstAsync(f => f.Id == usuario.Id, cancellationToken);
                if (user is null)
                {
                    Notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                    return null;
                }

                if (user.Type != TiposFuncionario.Gerente)
                {
                    Notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Funcionario), "criar um novo funcionário!"));
                    return null;
                }

                await Data.Funcionarios.AddAsync(funcionario, cancellationToken);
                await Data.SaveChangesAsync(cancellationToken);
                return funcionario;
            }
            catch (Exception e)
            {
                Notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await
                    All()
                    .FirstAsync(e => e.Id == id, cancellationToken);
                if (entity is null)
                {
                    Notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                    return false;
                }

                Data.Funcionarios.Remove(entity);
                await Data.SaveChangesAsync(cancellationToken);
                return true;

            }
            catch (Exception e)
            {
                Notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
        }

        public async Task<Funcionario> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await
                    All()
                    .FirstAsync(e => e.Id == id, cancellationToken);
                if (entity is null)
                    Notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                return entity;

            }
            catch (Exception e)
            {
                Notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }
        public Task<Funcionario> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
