using Restaurante.Application.BasicEntities.Requests.Common;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;

namespace Restaurante.Application.BasicEntities.Requests.Banks
{
    internal class BankRequestsHandler : BasicEntityRequestsHandler<Bank>
    {
        public BankRequestsHandler(IBasicEntitiesService basicEntitiesService, INotifier notifier) : base(basicEntitiesService, notifier)
        {
        }
    }
}
