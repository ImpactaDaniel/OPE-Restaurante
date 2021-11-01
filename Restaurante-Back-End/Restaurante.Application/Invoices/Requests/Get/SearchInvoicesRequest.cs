using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Invoices.Common.Models;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Models.Enum;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Invoices.Requests.Get
{
    public class SearchInvoicesRequest : InvoiceRequest<SearchInvoicesRequest>, IRequest<Response<PaginationInfo<Invoice>>>
    {
        public int CurrentUserId { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }

        internal class SearchInvoicesRequestHandler : IRequestHandler<SearchInvoicesRequest, Response<PaginationInfo<Invoice>>>
        {
            private readonly INotifier _notifier;
            private readonly IInvoiceDomainRepository _invoiceRespository;
            private readonly ILogger<SearchInvoicesRequestHandler> _logger;
            private readonly IEmployeesService<Employee> _employeesService;

            public SearchInvoicesRequestHandler(INotifier notifier, IInvoiceDomainRepository invoiceRespository, ILogger<SearchInvoicesRequestHandler> logger, IEmployeesService<Employee> employeesService)
            {
                _notifier = notifier;
                _invoiceRespository = invoiceRespository;
                _logger = logger;
                _employeesService = employeesService;
            }

            public async Task<Response<PaginationInfo<Invoice>>> Handle(SearchInvoicesRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var currentUser = await _employeesService.Get(request.CurrentUserId, cancellationToken);

                    if (currentUser is null)
                        throw new BasicTableException("Funcionário não encontrado!", Domain.Common.Enums.NotificationKeys.EntityNotFound);


                    return new Response<PaginationInfo<Invoice>>(true, await _invoiceRespository.GetAll(GetSearchExpression(request), request.Page, request.Limit, cancellationToken));

                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<PaginationInfo<Invoice>>(false, null);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new Exception("Houve um erro ao recuperar os pedidos!");
                }
            }

            private static Expression<Func<Invoice, bool>> GetSearchExpression(SearchInvoicesRequest request)
            {
                return request.Field switch
                {
                    "invoiceId" => invoice => invoice.Id == int.Parse(request.Value),
                    "customerId" => invoice => invoice.Customer.Id == int.Parse(request.Value),
                    "customerName" => invoice => invoice.Customer.Name.Contains(request.Value),
                    "cep" => invoice => invoice.Address.CEP.Contains(request.Value),
                    "status" => invoice => invoice.Status == (InvoiceStatus)int.Parse(request.Value),
                    _ => throw new BasicTableException("Filtro não implementado", Domain.Common.Enums.NotificationKeys.Error)
                };
            }
        }
    }
}
