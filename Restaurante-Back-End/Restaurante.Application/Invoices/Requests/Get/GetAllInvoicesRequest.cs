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
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Invoices.Requests.Get
{
    public class GetAllInvoicesRequest : InvoiceRequest<GetAllInvoicesRequest>, IRequest<Response<PaginationInfo<Invoice>>>
    {
        public int CurrentUserId { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }

        internal class GetAllInvoicesRequestHandler : IRequestHandler<GetAllInvoicesRequest, Response<PaginationInfo<Invoice>>>
        {
            private readonly INotifier _notifier;
            private readonly IInvoiceDomainRepository _invoiceRespository;
            private readonly ILogger<GetAllInvoicesRequestHandler> _logger;
            private readonly IEmployeesService<Employee> _employeesService;

            public GetAllInvoicesRequestHandler(INotifier notifier, IInvoiceDomainRepository invoiceRespository, ILogger<GetAllInvoicesRequestHandler> logger, IEmployeesService<Employee> employeesService)
            {
                _notifier = notifier;
                _invoiceRespository = invoiceRespository;
                _logger = logger;
                _employeesService = employeesService;
            }

            public async Task<Response<PaginationInfo<Invoice>>> Handle(GetAllInvoicesRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var currentUser = await _employeesService.Get(request.CurrentUserId, cancellationToken);

                    if (currentUser is null)
                        throw new BasicTableException("Funcionário não encontrado!", Domain.Common.Enums.NotificationKeys.EntityNotFound);


                    return new Response<PaginationInfo<Invoice>>(true, await _invoiceRespository.GetAll(request.Page, request.Limit, cancellationToken));

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
        }
    }
}
