using MediatR;
using Restaurante.Application.Common;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Employees.Requests.GetAll
{
    public class GetAllEmployeesRequest : IRequest<Response<PaginationInfo<Employee>>>
    {
        public int Limit { get; set; }
        public int Page { get; set; }
        internal class GetAllEmployeeRequestHandler : IRequestHandler<GetAllEmployeesRequest, Response<PaginationInfo<Employee>>>
        {
            private readonly IEmployeesService<Employee> _service;

            public GetAllEmployeeRequestHandler(IEmployeesService<Employee> service)
            {
                _service = service;
            }

            public async Task<Response<PaginationInfo<Employee>>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
            {
                return new Response<PaginationInfo<Employee>>(true, await _service.GetAll(request.Page, request.Limit, cancellationToken));
            }
        }
    }
}
