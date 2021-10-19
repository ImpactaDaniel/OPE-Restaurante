using Restaurante.Application.BasicEntities.Requests.Banks.Models;
using Restaurante.Application.BasicEntities.Requests.Common;
using Restaurante.Application.Common;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.BasicEntities.Requests.Banks
{
    internal class BankRequestsHandler : BasicEntityRequestsHandler<CreateBankRequest, Bank>
    {
        public BankRequestsHandler(IBasicEntitiesService basicEntitiesService, INotifier notifier) : base(basicEntitiesService, notifier)
        {
        }

        public async override Task<Response<bool>> Handle(CreateEntityRequest<CreateBankRequest, Bank> request, CancellationToken cancellationToken)
        {
            var newRequest = new CreateEntityRequest<CreateBankRequest, Bank>
            {
                Entity = new Bank(request.EntityRequest.Name)
            };
            return await base.Handle(newRequest, cancellationToken);
        }
    }
}
