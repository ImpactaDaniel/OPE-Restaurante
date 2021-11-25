using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Factories.Interfaces;
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
            private readonly IBasicEntitiesService _basicEntitiesService;
            private readonly INotifier _notifier;
            private readonly IMessageSenderService<EmailMessage> _emailService;

            public CreateEmployeeRequestHandler(IEmployeeFactory factory,
                                                   IEmployeesService<Employee> service,
                                                   INotifier notifier,
                                                   IMessageSenderService<EmailMessage> emailService,
                                                   IBasicEntitiesService basicEntitiesService)
            {
                _factory = factory;
                _service = service;
                _notifier = notifier;
                _basicEntitiesService = basicEntitiesService;
                _emailService = emailService;
            }

            public async Task<Response<bool>> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var bank = await _basicEntitiesService.GetEntity<Bank>(b => b.Id == request.Account.Bank.BankId, cancellationToken);

                    if (bank is null)
                        throw new UserException("Banco não encontrado!", NotificationKeys.InvalidEntity);

                    var account = new Account(bank, request.Account.Branch, request.Account.AccountNumber, request.Account.Digit);

                    var address = new Address(request.Address.Street, request.Address.Number, request.Address.District, request.Address.CEP, request.Address.State, request.Address.City);

                    var phones = request.Phones.Select(p => new Phone(p.DDD, p.PhoneNumber));

                    _factory
                        .WithAccount(account)
                        .WithAddress(address)
                        .WithPhones(phones)
                        .WithDocument(request.Document)
                        .WithBirthDate(request.BirthDate)
                        .WithType(Domain.Users.Enums.UsersType.Employee)
                        .WithName(request.Name)
                        .WithEmail(request.Email)
                        .WithPassword(request.Password);


                    var employee = _factory
                        .Build();

                    var password = employee.Password;

                    var created = await _service.CreateEmployee(employee, request.CurrentUser, cancellationToken);

                    if (!created)
                        return new Response<bool>(!_notifier.HasNotifications(), false);

                    var responseEmail = await _emailService.SendAsync(new EmailMessage(employee.Email, "Your new Credentials", $"E-mail: {employee.Email} Senha: {password}"), cancellationToken);

                    return new Response<bool>(!_notifier.HasNotifications(), created);
                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<bool>(false, false);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        #endregion
    }
}
