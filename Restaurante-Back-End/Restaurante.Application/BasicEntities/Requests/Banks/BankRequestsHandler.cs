using Restaurante.Application.BasicEntities.Requests.Banks.Models;
using Restaurante.Application.BasicEntities.Requests.Common;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Data.Models;
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

        public async override Task<Response<PaginationInfo<Bank>>> Handle(SearchEntitiesRequest<Bank> request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _basicEntitiesService.GetEntities<Bank>(bank => bank.Name.Contains(request.Value), request.Limit * request.Page, request.Limit, cancellationToken);
                return new Response<PaginationInfo<Bank>>(true, res);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return new Response<PaginationInfo<Bank>>(false, null);
            }
        }

        public async override Task<Response<Bank>> Handle(GetEntityRequest<Bank> request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _basicEntitiesService.GetEntity<Bank>(bank => bank.Id == request.Id, cancellationToken);
                return new Response<Bank>(true, res);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return new Response<Bank>(false, null);
            }
        }
    }
}
