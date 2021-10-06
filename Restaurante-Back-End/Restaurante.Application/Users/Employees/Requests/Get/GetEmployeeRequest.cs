using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Employees.Requests.Get
{
    public class GetEmployeeRequest : EmployeeRequest<GetEmployeeRequest>, IRequest<Response<Employee>>
    {
        internal class GetEmployeeRequestHandler : IRequestHandler<GetEmployeeRequest, Response<Employee>>
        {
            private readonly IEmployeesService<Employee> _service;

            public GetEmployeeRequestHandler(IEmployeesService<Employee> service)
            {
                _service = service;
            }

            public async Task<Response<Employee>> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
            {
                var employee = await _service.Get(request.Id, cancellationToken);

                if (employee is null)
                    return new Response<Employee>(false, null);

                return new Response<Employee>(true, employee);
            }
        }
    }
}
