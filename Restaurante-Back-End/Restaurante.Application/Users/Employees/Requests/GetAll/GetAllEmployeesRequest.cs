using MediatR;
using Restaurante.Application.Common;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Employees.Requests.GetAll
{
    public class GetAllEmployeesRequest : IRequest<Response<IList<Employee>>>
    {
        internal class GetAllEmployeeRequestHandler : IRequestHandler<GetAllEmployeesRequest, Response<IList<Employee>>>
        {
            private readonly IEmployeesService<Employee> _service;

            public GetAllEmployeeRequestHandler(IEmployeesService<Employee> service)
            {
                _service = service;
            }

            public async Task<Response<IList<Employee>>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
            {
                return new Response<IList<Employee>>(true, await _service.GetAll(cancellationToken));
            }
        }
    }
}
