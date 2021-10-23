using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Infra.Invoices.Repositories
{
    public class PaymentRepository : DataRepository<IRestauranteDbContext, Payment>, IPaymentDomainRepository
    {
        public PaymentRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<Payment> CreatePayment(Payment payment, CancellationToken cancellationToken = default)
        {
            await Data.Payments.AddAsync(payment, cancellationToken);
            await Data.SaveChangesAsync(cancellationToken);
            return payment;
        }

        public async Task<IList<Payment>> GetAll(CancellationToken cancellationToken = default) =>
            await All()
                    .Include(p => p.Customer)
                  .ToListAsync(cancellationToken);
    }
}
