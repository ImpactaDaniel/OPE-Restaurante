using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Entregadores.Services.Interfaces;
using Restaurante.Domain.Users.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Deliveries.Requests.Create
{
    public class CreateDeliverymanRequest : DeliverymanRequest<CreateDeliverymanRequest>, IRequest<Response<bool>>
    {
        public int CurrentUser { get; set; }
        internal class CreateDeliverymanRequestHandler : IRequestHandler<CreateDeliverymanRequest, Response<bool>>
        {
            private readonly IDeliveryPersonService _deliveryPersonService;
            private readonly INotifier _notifier;
            private readonly ILogger<CreateDeliverymanRequestHandler> _logger;
            private readonly IDeliverFactory _factory;
            private readonly IBasicEntitiesService _basicEntitiesService;
            private readonly IMessageSenderService<EmailMessage> _emailService;
            public CreateDeliverymanRequestHandler(
                IDeliveryPersonService deliveryPersonService,
                INotifier notifier,
                ILogger<CreateDeliverymanRequestHandler> logger,
                IDeliverFactory factory,
                IBasicEntitiesService basicEntitiesService,
                IMessageSenderService<EmailMessage> emailService)
            {
                _deliveryPersonService = deliveryPersonService;
                _notifier = notifier;
                _logger = logger;
                _factory = factory;
                _basicEntitiesService = basicEntitiesService;
                _emailService = emailService;
            }

            public async Task<Response<bool>> Handle(CreateDeliverymanRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var bank = await
                        _basicEntitiesService.GetEntity<Bank>(bank => bank.Id == request.Account.Bank.BankId, cancellationToken);

                    if (bank is null)
                        throw new UserException("Banco não encontrado!", NotificationKeys.InvalidEntity);

                    var account = new Account(bank, request.Account.Branch, request.Account.AccountNumber, request.Account.Digit);

                    var address = new Address(request.Address.Street, request.Address.Number, request.Address.District, request.Address.CEP);

                    var phones = request.Phones.Select(p => new Phone(p.DDD, p.PhoneNumber));

                    var vehicle = new Vehicle(request.Vehicle.MotorcycleModel, request.Vehicle.MotorcycleBrand, request.Vehicle.MotorcycleYear);

                    _factory
                        .WithAccount(account)
                        .WithAddress(address)
                        .WithPhones(phones)
                        .WithVehicle(vehicle)
                        .WithName(request.Name)
                        .WithPassword(request.Password)
                        .WithEmail(request.Email);

                    var deliveryman = _factory
                        .Build();

                    var success = await _deliveryPersonService.CreateEmployee(deliveryman, request.CurrentUser, cancellationToken);

                    await _emailService.SendAsync(new EmailMessage(deliveryman.Email, "Your new credentials", $"{deliveryman.Email}, {deliveryman.Password}"), cancellationToken);

                    return new Response<bool>(success, success);
                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<bool>(false, false);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    return new Response<bool>(false, false);
                }
            }
        }
    }
}
