using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Invoices.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Invoices.Repositories.Interfaces
{
    public interface IPaymentDomainRepository : IDomainRepository<Payment>
    {
        Task<Payment> CreatePayment(Payment payment, CancellationToken cancellationToken = default);
        Task<IList<Payment>> GetAll(CancellationToken cancellationToken = default);
    }
}
