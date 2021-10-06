﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Users.Employees.Requests.Create;
using Restaurante.Application.Users.Employees.Requests.Get;
using Restaurante.Application.Users.Employees.Requests.GetAll;
using Restaurante.Domain.Common.Services.Interfaces;
    using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [ApiController, Route("[controller]")]
    public class EmployeesController : APIControllerBase
    {
        public EmployeesController(IMediator mediatr, INotifier notifier)
            : base(notifier, mediatr)
        {
        }
        [Route("GetAll"), HttpGet, Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var resp = await _mediator.Send(new GetAllEmployeesRequest(), cancellationToken);
            return GetResponse(resp.Result);
        }

        [Route("Get"), HttpGet, Authorize]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var resp = await _mediator.Send(new GetEmployeeRequest() { Id = GetLoggedUserId() }, cancellationToken);
            return GetResponse(resp.Result);
        }

        [Route("Create"), HttpPost, Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateNew([FromBody] CreateEmployeeRequest request, CancellationToken cancellationToken = default)
        {
            request.CurrentUser = GetLoggedUserId();
            var resp = await _mediator.Send(request, cancellationToken);
            return GetResponse(resp.Result);
        }
    }
}
