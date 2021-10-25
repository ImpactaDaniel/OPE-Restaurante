using Microsoft.AspNetCore.Http;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurante.Web.Middlewares
{
    public class RequestMiddleware : IMiddleware
    {
        private readonly IEmployeesService<Employee> _service;
        private readonly INotifier _notifier;

        public RequestMiddleware(IEmployeesService<Employee> service, INotifier notifier)
        {
            _service = service;
            _notifier = notifier;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var id = context.User.Claims.First(c => c.Type == ClaimTypes.Sid)?.Value;
                if (!string.IsNullOrEmpty(id))
                {
                    var user = await _service.Get(int.Parse(id));
                    if (user.FirstAccess && !context.Request.Path.Value?.Contains("ChangePasswordFirstAccess") == true)
                    {
                        context.Response.Clear();
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        _notifier.AddNotification(new Notification((int)NotificationKeys.Error, "É necessário alterar a senha antes de continuar!"));
                        await context.Response.WriteAsJsonAsync(_notifier.GetNotifications());
                        return;
                    }
                }

            }

            await next(context);
        }
    }
}
