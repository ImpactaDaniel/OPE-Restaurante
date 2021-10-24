﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Invoices.Common.Models;
using Restaurante.Application.Invoices.Notifications;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using Restaurante.Domain.Users.Customers.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Invoices.Requests.Create
{
    public class CreateInvoiceRequest : InvoiceRequest<CreateInvoiceRequest>, IRequest<Response<Invoice>>
    {
        internal class CreateInvoiceRequestHandler : IRequestHandler<CreateInvoiceRequest, Response<Invoice>>
        {
            private readonly INotifier _notifier;
            private readonly IInvoiceDomainRepository _invoiceRespository;
            private readonly ICustomersDomainRepository _customersDomainRepository;
            private readonly IProductService _productService;
            private readonly ILogger<CreateInvoiceRequestHandler> _logger;
            private readonly IMediator _mediator;

            public CreateInvoiceRequestHandler(INotifier notifier, IInvoiceDomainRepository invoiceRespository, ICustomersDomainRepository customersDomainRepository, IProductService productService, ILogger<CreateInvoiceRequestHandler> logger, IMediator mediator)
            {
                _notifier = notifier;
                _invoiceRespository = invoiceRespository;
                _customersDomainRepository = customersDomainRepository;
                _productService = productService;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Response<Invoice>> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var lines = await GetInvoiceLines(request.Products, cancellationToken);

                    var customer = await _customersDomainRepository.Get(request.CustomerId, cancellationToken);

                    if (customer is null)
                        throw new BasicTableException($"Cliente {request.CustomerId} não existe!", NotificationKeys.EntityNotFound);

                    var address = customer.Addresses.Where(a => a.Id == request.CustomerAddress).FirstOrDefault();

                    if (address is null)
                        throw new BasicTableException($"Endereço {request.CustomerAddress} não existe!", NotificationKeys.EntityNotFound);

                    var invoice = new Invoice
                    {
                        Address = address,
                        Customer = customer,
                        Products = lines.ToList(),
                        Status = Domain.Invoices.Models.Enum.InvoiceStatus.Created
                    };

                    _ = await _invoiceRespository.CreateInvoice(invoice, cancellationToken);

                    await _mediator.Publish(new InvoiceNotification
                    {
                        Invoice = invoice,
                        NotificationType = Common.Models.Enums.InvoiceNotificationType.Updated
                    }, cancellationToken);

                    return new Response<Invoice>(true, invoice);

                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<Invoice>(false, null);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new Exception("Houve um erro ao criar o pedido!");
                }
            }

            private async Task<IEnumerable<InvoiceLine>> GetInvoiceLines(IEnumerable<ProductInvoiceRequest> productsRequest, CancellationToken cancellationToken = default)
            {
                var lines = new List<InvoiceLine>(productsRequest.Count());

                foreach (var productRequest in productsRequest)
                {
                    var product = await _productService.Get(productRequest.Id, cancellationToken);
                    if (product is null)
                        throw new BasicTableException($"Produto {productRequest.Id} não existe!", NotificationKeys.EntityNotFound);

                    lines.Add(new InvoiceLine
                    {
                        Product = product,
                        Quantity = productRequest.Quantity,
                        Obs = productRequest.Obs
                    });
                }

                return lines;
            }
        }
    }
}
