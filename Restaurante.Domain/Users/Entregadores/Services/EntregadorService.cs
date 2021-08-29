using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Entregadores.Models;

namespace Restaurante.Domain.Users.Funcionarios.Services
{
    internal class EntregadorService : FuncionarioService<Entregador>
    {
        public EntregadorService(INotifier notifier, Repositories.IFuncionarioDomainRepository<Entregador> repository)
            : base(notifier, repository)
        {
        }
    }
}
