using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Invoices.Repositories
{
    public class InvoiceRepository : DataRepository<IRestauranteDbContext, Invoice>, IInvoiceDomainRepository
    {
        public InvoiceRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public Task<Invoice> CreateInvoice(Invoice invoice, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Invoice invoice, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Invoice>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Invoice>> GetAll(Customer customer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
