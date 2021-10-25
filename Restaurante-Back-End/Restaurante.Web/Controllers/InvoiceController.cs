using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Invoices.Requests.Create;
using Restaurante.Application.Invoices.Requests.Get;
using Restaurante.Application.Invoices.Requests.Update;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Invoices.Models.Enum;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [Route("[controller]")]
    public class InvoiceController : APIControllerBase
    {
        public InvoiceController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> CreateNewInvoice([FromBody] CreateInvoiceRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("GetAll"), Authorize]
        public async Task<IActionResult> GetAllInvoices(CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetAllInvoicesRequest { CurrentUserId = GetLoggedUserId() }, cancellationToken);

            return GetResponse(response);
        }

        [HttpPatch, Route("UpdateStatus/{id}"), Authorize]
        public async Task<IActionResult> UpdateInvoiceStatus(int id, InvoiceStatus status, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new UpdateInvoiceStatusRequest { Id = id, Status = status }, cancellationToken);
            return GetResponse(response);
        }
    }
}
