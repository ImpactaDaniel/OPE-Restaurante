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
    public class GetInvoicesByCustomerRequest : InvoiceRequest<SearchInvoicesRequest>, IRequest<Response<IList<Invoice>>>
    {
        public int CustomerId { get; set; }

        internal class GetInvoicesByCustomerRequestHandler : IRequestHandler<GetInvoicesByCustomerRequest, Response<IList<Invoice>>>
        {
            private readonly INotifier _notifier;
            private readonly IInvoiceDomainRepository _invoiceRespository;
            private readonly ILogger<GetInvoicesByCustomerRequestHandler> _logger;
            private readonly IEmployeesService<Employee> _employeesService;

            public GetInvoicesByCustomerRequestHandler(INotifier notifier, IInvoiceDomainRepository invoiceRespository, ILogger<GetInvoicesByCustomerRequestHandler> logger, IEmployeesService<Employee> employeesService)
            {
                _notifier = notifier;
                _invoiceRespository = invoiceRespository;
                _logger = logger;
                _employeesService = employeesService;
            }

            public async Task<Response<IList<Invoice>>> Handle(GetInvoicesByCustomerRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<IList<Invoice>>(true, await _invoiceRespository.GetAll(c => c.CustomerId == request.CustomerId, cancellationToken));

                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<IList<Invoice>>(false, null);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new Exception("Houve um erro ao recuperar os pedidos!");
                }
            }
        }
    }
}
