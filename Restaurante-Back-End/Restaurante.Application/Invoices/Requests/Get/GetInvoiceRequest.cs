using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Invoices.Common.Models;
using Restaurante.Domain.BasicEntities.Exception;
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
    public class GetInvoiceRequest : InvoiceRequest<GetInvoiceRequest>, IRequest<Response<Invoice>>
    {
        public int CurrentUserId { get; set; }

        internal class GetInvoiceRequestHandler : IRequestHandler<GetInvoiceRequest, Response<Invoice>>
        {
            private readonly INotifier _notifier;
            private readonly IInvoiceDomainRepository _invoiceRespository;
            private readonly ILogger<GetInvoiceRequestHandler> _logger;
            private readonly IEmployeesService<Employee> _employeesService;

            public GetInvoiceRequestHandler(INotifier notifier, IInvoiceDomainRepository invoiceRespository, ILogger<GetInvoiceRequestHandler> logger, IEmployeesService<Employee> employeesService)
            {
                _notifier = notifier;
                _invoiceRespository = invoiceRespository;
                _logger = logger;
                _employeesService = employeesService;
            }

            public async Task<Response<Invoice>> Handle(GetInvoiceRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var currentUser = await _employeesService.Get(request.CurrentUserId, cancellationToken);

                    if (currentUser is null)
                        throw new BasicTableException("Funcionário não encontrado!", Domain.Common.Enums.NotificationKeys.EntityNotFound);


                    return new Response<Invoice>(true, await _invoiceRespository.Get(i => i.Id == request.Id, cancellationToken));

                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<Invoice>(false, null);
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
