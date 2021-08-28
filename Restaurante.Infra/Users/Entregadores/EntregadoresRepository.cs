using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Entregadores;
using Restaurante.Domain.Users.Funcionarios;
using Restaurante.Domain.Users.Repositories.Interfaces;
using Restaurante.Infra.Common.Helper;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Domain.Users.Enums;

namespace Restaurante.Infra.Users.Entregadores
{
    internal class EntregadoresRepository :
        DataRepository<IRestauranteDbContext, Entregador>,
        IEntregadorDomainRepository
    {
        public EntregadoresRepository(IRestauranteDbContext db, INotifier notifier) : base(db, notifier)
        {
        }

        public async Task<Entregador> CreateFuncionario(Entregador entregador, Funcionario usuario, CancellationToken cancellationToken = default)
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
                    Notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Funcionario), "criar um novo entregador!"));
                    return null;
                }

                await Data.Entregadores.AddAsync(entregador, cancellationToken);
                await Data.SaveChangesAsync(cancellationToken);
                return entregador;
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
                    Notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Entregador)));
                    return false;
                }

                Data.Entregadores.Remove(entity);
                await Data.SaveChangesAsync(cancellationToken);
                return true;

            }
            catch (Exception e)
            {
                Notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
        }

        public async Task<Entregador> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await
                    All()
                    .FirstAsync(e => e.Id == id, cancellationToken);
                if (entity is null)
                    Notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Entregador)));
                return entity;

            }
            catch (Exception e)
            {
                Notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
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
