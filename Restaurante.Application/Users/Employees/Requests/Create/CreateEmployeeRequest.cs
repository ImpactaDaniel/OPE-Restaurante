using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Linq;
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
            private readonly IDefaultDomainRepository _defaultRepository;
            private readonly INotifier _notifier;
            private readonly ILogger<CreateEmployeeRequestHandler> _logger;
            private readonly IMessageSenderService<EmailMessage> _emailService;

            public CreateEmployeeRequestHandler(IEmployeeFactory factory,
                                                   IEmployeesService<Employee> service,
                                                   INotifier notifier,
                                                   ILogger<CreateEmployeeRequestHandler>  logger,
                                                   IMessageSenderService<EmailMessage> emailService,
                                                   IDefaultDomainRepository defaulRepository)
            {
                _factory = factory;
                _service = service;
                _notifier = notifier;
                _logger = logger;
                _defaultRepository = defaulRepository;
                _emailService = emailService;
            }

            public async Task<Response<bool>> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var bank = await _defaultRepository.Get<Bank>(b => b.Id == request.Bank.BankId, cancellationToken);

                    if (bank is null)
                        throw new UserException("Banco não encontrado!");

                    var account = new Account(bank, request.Account.Branch, request.Account.AccountNumber, request.Account.Digit);

                    var address = new Address(request.Address.Street, request.Address.Number, request.Address.District, request.Address.CEP);

                    var phones = request.Phones.Select(p => new Phone(p.DDD, p.PhoneNumber));

                    _factory
                        .WithAccount(account)
                        .WithAddress(address)
                        .WithPhones(phones)
                        .WithType(Domain.Users.Enums.EmployeesType.Employee)
                        .WithName(request.Name)
                        .WithEmail(request.Email)
                        .WithPassword(request.Password);
                        

                    var employee = _factory
                        .Build();

                    var func = await _service.CreateEmployee(employee, request.CurrentUser, cancellationToken);

                    var responseEmail = await _emailService.SendAsync(new EmailMessage(employee.Email, "Your new Credentials", $"E-mail: {employee.Email} Senha: {employee.Password}"), cancellationToken);

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
