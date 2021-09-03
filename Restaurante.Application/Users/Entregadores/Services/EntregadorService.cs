using Microsoft.Extensions.Logging;
using Restaurante.Application.Users.Funcionarios.Services;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Entregadores.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Repositories;

namespace Restaurante.Application.Users.Entregadores.Services
{
    internal class EntregadorService : FuncionarioService<Entregador>, IEntregadoresService
    {
        public EntregadorService(INotifier notifier, IFuncionarioDomainRepository<Entregador> repository, ILogger<EntregadorService> logger)
            : base(notifier, repository, logger)
        {
        }
    }
}
