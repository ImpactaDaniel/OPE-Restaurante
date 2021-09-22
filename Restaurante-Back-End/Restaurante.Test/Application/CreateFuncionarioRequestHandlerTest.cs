using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Application.Users.Employees.Requests.Create;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using static Restaurante.Application.Users.Employees.Requests.Create.CreateEmployeeRequest;

namespace Restaurante.Test.Application
{
    public class CreateFuncionarioRequestHandlerTest
    {
        private readonly IEmployeeFactory _factory;
        private readonly IEmployeesService<Employee> _service;
        private readonly IMessageSenderService<EmailMessage> _emailService;
        private readonly INotifier _notifier;
        private readonly ILogger<CreateEmployeeRequestHandler> _logger;
        private readonly IBasicEntitiesService _basicEntitiesService;

        public CreateFuncionarioRequestHandlerTest()
        {
            _factory = Substitute.For<IEmployeeFactory>();
            _factory.Build().ReturnsForAnyArgs(EmployeeMock.GetDefault());

            _service = Substitute.For<IEmployeesService<Employee>>();

            _emailService = Substitute.For<IMessageSenderService<EmailMessage>>();

            _logger = Substitute.For<ILogger<CreateEmployeeRequestHandler>>();

            _basicEntitiesService = Substitute.For<IBasicEntitiesService>();

            _basicEntitiesService.GetEntity(Arg.Any<Expression<Func<Bank, bool>>>())
                .ReturnsForAnyArgs(new Bank("teste"));

            _notifier = Substitute.For<INotifier>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateEmployeeRequestHandler(_factory, _service, _notifier, _logger, _emailService, _basicEntitiesService);
            var funcionarioDefault = EmployeeMock.GetDefault();
            var phones = new List<PhoneRequest>()
            {
                new PhoneRequest()
                {
                    DDD = "22",
                    PhoneNumber = "9999999"
                }
            };
            var request = new CreateEmployeeRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type,
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
            };
            _service.CreateEmployee(default, default).ReturnsForAnyArgs(true);

            _notifier.HasNotifications().ReturnsForAnyArgs(false);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(response.Result);
            Assert.True(response.Success);
            _notifier.ReceivedWithAnyArgs().HasNotifications();
            await _emailService.ReceivedWithAnyArgs().SendAsync(Arg.Any<EmailMessage>());
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateEmployeeRequestHandler(_factory, _service, _notifier, _logger, _emailService, _basicEntitiesService);
            var funcionarioDefault = EmployeeMock.GetDefault();
            var phones = new List<PhoneRequest>()
            {
                new PhoneRequest()
                {
                    DDD = "22",
                    PhoneNumber = "9999999"
                }
            };
            var request = new CreateEmployeeRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type,
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
            };

            _service.CreateEmployee(Arg.Any<Employee>(), default).ReturnsForAnyArgs(false);

            _notifier.HasNotifications().ReturnsForAnyArgs(true);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.False(response.Result);
            Assert.False(response.Success);
            _notifier.ReceivedWithAnyArgs().HasNotifications();
        }
    }
}
