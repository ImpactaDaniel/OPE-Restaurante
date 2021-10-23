using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Invoices.Repositories
{
    public class PaymentRepository : DataRepository<IRestauranteDbContext, Payment>, IPaymentDomainRepository
    {
        public PaymentRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public Task<Payment> CreatePayment(Payment payment, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Payment>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
