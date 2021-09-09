using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Employees.Requests.Create
{
    public class CreateEmployeeRequest : EmployeeRequest<CreateEmployeeRequest>, IRequest<Response<bool>>
    {
        public int CurrentUser { get; set; }

        #region Handler

        internal class CreateEmployeeRequestHandler : IRequestHandler<CreateEmployeeRequest, Response<bool>>
        {
            private readonly IEmployeeFactory _factory;
            private readonly IEmployeesService<Employee> _service;
            private readonly INotifier _notifier;
            private readonly ILogger<CreateFuncionarioRequestHandler> _logger;
            private readonly IMessageSenderService<EmailMessage> _emailService;

            public CreateFuncionarioRequestHandler(IFuncionarioFactory factory,
                                                   IFuncionarioService<Funcionario> service,
                                                   INotifier notifier,
                                                   ILogger<CreateFuncionarioRequestHandler>  logger,
                                                   IMessageSenderService<EmailMessage> emailService)
            {
                _factory = factory;
                _service = service;
                _notifier = notifier;
                _logger = logger;
                _emailService = emailService;
            }

            public async Task<Response<bool>> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    _factory
                        .WithType(request.Type)
                        .WithName(request.Name)
                        .WithEmail(request.Email)
                        .WithPassword(request.Password);

                    var employee = _factory
                        .Build();

                    var func = await _service.CreateFuncionario(funcionario, request.CurrentUser, cancellationToken);

                    var responseEmail = await _emailService.SendAsync(new EmailMessage(funcionario.Email, "Your new Credentials", $"E-mail: {funcionario.Email} Senha: {funcionario.Password}"));

                    return new Response<bool>(!_notifier.HasNotifications(), func);
                }
                catch (UserException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<bool>(false, false);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new Exception("Houve um erro ao tentar criar o funcionário!", e);
                }

            }
        }
        #endregion
    }
}
