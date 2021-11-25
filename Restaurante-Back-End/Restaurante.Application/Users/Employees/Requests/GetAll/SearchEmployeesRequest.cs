using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Employees.Requests.GetAll
{
    public class SearchEmployeesRequest : IRequest<Response<PaginationInfo<Employee>>>
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        internal class SearchEmployeeRequestHandler : IRequestHandler<SearchEmployeesRequest, Response<PaginationInfo<Employee>>>
        {
            private readonly IEmployeesService<Employee> _service;
            private readonly INotifier _notifier;
            public SearchEmployeeRequestHandler(IEmployeesService<Employee> service, INotifier notifier)
            {
                _service = service;
                _notifier = notifier;
            }
            public async Task<Response<PaginationInfo<Employee>>> Handle(SearchEmployeesRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<PaginationInfo<Employee>>(true, await _service.Search(GetExpression(request.Field, request.Value), request.Page, request.Limit, cancellationToken));
                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<PaginationInfo<Employee>>(false, null);
                }
            }

            private Expression<Func<Employee, bool>> GetExpression(string field, string value)
            {
                return field switch
                {
                    "email" => e => e.Email.Contains(value),
                    "name" => e => e.Name.Contains(value),
                    "type" => e => e.Type == (UsersType)int.Parse(value),
                    _ => throw new BasicTableException("Filtro não implementado!", Domain.Common.Enums.NotificationKeys.Error),
                };
            }
        }
    }
}
