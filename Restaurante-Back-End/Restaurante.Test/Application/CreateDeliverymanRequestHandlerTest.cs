using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Application.Users.Deliveries.Requests.Create;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Entregadores.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using static Restaurante.Application.Users.Deliveries.Requests.Create.CreateDeliverymanRequest;

namespace Restaurante.Test.Application
{
    public class CreateDeliverymanRequestHandlerTest
    {
        private readonly IDeliverFactory _factory;
        private readonly IDeliveryPersonService _service;
        private readonly IMessageSenderService<EmailMessage> _emailService;
        private readonly INotifier _notifier;
        private readonly ILogger<CreateDeliverymanRequestHandler> _logger;
        private readonly IBasicEntitiesService _basicEntitiesService;

        public CreateDeliverymanRequestHandlerTest()
        {
            _factory = Substitute.For<IDeliverFactory>();
            _factory.Build().ReturnsForAnyArgs(EntregadorMock.GetDefaulEntregador());

            _service = Substitute.For<IDeliveryPersonService>();

            _emailService = Substitute.For<IMessageSenderService<EmailMessage>>();

            _logger = Substitute.For<ILogger<CreateDeliverymanRequestHandler>>();

            _basicEntitiesService = Substitute.For<IBasicEntitiesService>();

            _basicEntitiesService.GetEntity(Arg.Any<Expression<Func<Bank, bool>>>())
                .ReturnsForAnyArgs(new Bank("teste"));

            _notifier = Substitute.For<INotifier>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateDeliverymanRequestHandler(_service, _notifier, _logger, _factory, _basicEntitiesService, _emailService);
            var funcionarioDefault = EmployeeMock.GetDefault();
            var phones = new List<PhoneRequest>()
            {
                new PhoneRequest()
                {
                    DDD = "22",
                    PhoneNumber = "9999999"
                }
            };
            var request = new CreateDeliverymanRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type,
                Document = DataTest.CPF,
                Account = new AccountRequest()
                {
                    Branch = "teste",
                    AccountNumber = "teste",
                    Digit = 1,
                    Bank = new BankRequest()
                    {
                        BankId = 0,
                    },
                },
                Address = new AddressRequest()
                {
                    CEP = "02998190",
                    District = "Hata",
                    Street = "Haerar",
                    Number = "1",
                    State = "SP",
                    City = "São Paulo"
                },
                CurrentUser = 0,
                Phones = phones,
                Vehicle = new VehicleRequest
                {
                    MotorcycleBrand = "Suzuki",
                    MotorcycleModel = "Kawasaki",
                    MotorcycleYear = 2010
                }
            };
            _service.CreateEmployee(default, default).ReturnsForAnyArgs(true);

            _notifier.HasNotifications().ReturnsForAnyArgs(false);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(response.Result);
            Assert.True(response.Success);
            await _emailService.ReceivedWithAnyArgs().SendAsync(Arg.Any<EmailMessage>());
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateDeliverymanRequestHandler(_service, _notifier, _logger, _factory, _basicEntitiesService, _emailService);
            var funcionarioDefault = EmployeeMock.GetDefault();
            var phones = new List<PhoneRequest>()
            {
                new PhoneRequest()
                {
                    DDD = "22",
                    PhoneNumber = "9999999"
                }
            };
            var request = new CreateDeliverymanRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type,
                Document = DataTest.CPF,
                Account = new AccountRequest()
                {
                    Branch = "teste",
                    AccountNumber = "teste",
                    Digit = 1,
                    Bank = new BankRequest()
                    {
                        BankId = 0,
                    },
                },
                Address = new AddressRequest()
                {
                    CEP = "02998190",
                    District = "Hata",
                    Street = "Haerar",
                    Number = "1",
                },
                CurrentUser = 0,
                Phones = phones,
                Vehicle = new VehicleRequest
                {
                    MotorcycleBrand = "Suzuki",
                    MotorcycleModel = "Kawasaki",
                    MotorcycleYear = 2010
                }
            };

            _service.CreateEmployee(Arg.Any<DeliveryPerson>(), default).ReturnsForAnyArgs(false);

            _notifier.HasNotifications().ReturnsForAnyArgs(true);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.False(response.Result);
            Assert.False(response.Success);
        }
    }
}
