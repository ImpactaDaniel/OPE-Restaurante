using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Common;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios;
using Restaurante.Domain.Users.Repositories.Interfaces;
using Restaurante.Infra.Common.Helper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Create
{
    public class CreateFuncionarioRequest : FuncionarioRequest<CreateFuncionarioRequest>, IRequest<Response<Funcionario>>
    {
        public Funcionario CurrentUser { get; set; }
        public CreateFuncionarioRequest(Funcionario funcionario) : base(funcionario)
        {
        }

        #region Handler        

        internal class CreateFuncionarioRequestHandler : IRequestHandler<CreateFuncionarioRequest, Response<Funcionario>>
        {
            private readonly IFuncionarioFactory<Funcionario> _factory;
            private readonly IFuncionarioDomainRepository<Funcionario> _repository;
            private readonly INotifier _notifier;

            public CreateFuncionarioRequestHandler(IFuncionarioFactory<Funcionario> factory,
                                                   IFuncionarioDomainRepository<Funcionario> repository,
                                                   INotifier notifier)
            {
                _factory = factory;
                _repository = repository;
                _notifier = notifier;
            }

            public async Task<Response<Funcionario>> Handle(CreateFuncionarioRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    _factory
                        .WithType(request.Type)
                        .WithName(request.Name)
                        .WithEmail(request.Email)
                        .WithPassword(request.Password);

                    var funcionario = _factory
                        .Build();

                    var func = await _repository.CreateFuncionario(funcionario, request.CurrentUser, cancellationToken);
                    return new Response<Funcionario>(!_notifier.HasNotifications(), func);

                }
                catch (Exception e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<Funcionario>(false, null);
                }

            }
        }
        #endregion
    }
}
