using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Common.Requests.Auth
{
    public class ChangePasswordFirstAccessRequest : AuthenticationRequest<ChangePasswordFirstAccessRequest>, IRequest<Response<User>>
    {
        public string OldPassword { get; set; }

        internal class ChangePasswordFirstAccessRequestHandler : IRequestHandler<ChangePasswordFirstAccessRequest, Response<User>>
        {
            private readonly IEmployeesService<Employee> _employeesService;

            public ChangePasswordFirstAccessRequestHandler(IEmployeesService<Employee> employeesService)
            {
                _employeesService = employeesService;
            }

            public async Task<Response<User>> Handle(ChangePasswordFirstAccessRequest request, CancellationToken cancellationToken)
            {
                var user = await _employeesService.Get(request.Id, cancellationToken);

                if (user is null)
                    return new Response<User>(false, null);

                var login = await _employeesService.Login(user.Email, request.OldPassword, cancellationToken);

                if (login is null)
                    return new Response<User>(false, null);

                login.UpdatePassword(request.Password);
                login.FirstAccess = false;

                var changed = await _employeesService.Update(login, cancellationToken);

                if (!changed)
                    return new Response<User>(false, null);

                return new Response<User>(changed, login);
            }
        }
    }
}
