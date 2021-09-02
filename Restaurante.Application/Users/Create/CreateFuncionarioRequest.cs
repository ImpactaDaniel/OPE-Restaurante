using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Users.Common;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Create
{
    public class CreateFuncionarioRequest : FuncionarioRequest<CreateFuncionarioRequest>, IRequest<Response<bool>>
    {
        public int CurrentUser { get; set; }

        #region Handler        

        internal class CreateFuncionarioRequestHandler : IRequestHandler<CreateFuncionarioRequest, Response<bool>>
        {
            private readonly IFuncionarioFactory _factory;
            private readonly IFuncionarioService<Funcionario> _service;
            private readonly INotifier _notifier;
            private readonly ILogger<CreateFuncionarioRequestHandler> _logger;
            public CreateFuncionarioRequestHandler(IFuncionarioFactory factory,
                                                   IFuncionarioService<Funcionario> service,
                                                   INotifier notifier,
                                                   ILogger<CreateFuncionarioRequestHandler>  logger)
            {
                _factory = factory;
                _service = service;
                _notifier = notifier;
                _logger = logger;
            }

            public async Task<Response<bool>> Handle(CreateFuncionarioRequest request, CancellationToken cancellationToken)
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

                    var func = await _service.CreateFuncionario(funcionario, request.CurrentUser, cancellationToken);
                    return new Response<bool>(!_notifier.HasNotifications(), func);

                }
                catch (UserException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<bool>(false, false);
                }
                catch(Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new Exception("Houve um erro ao tentar criar o funcionário!", e);
                }

            }
        }
        #endregion
    }
}
