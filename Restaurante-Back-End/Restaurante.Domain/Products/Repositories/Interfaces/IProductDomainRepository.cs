﻿using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Products.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Products.Repositories.Interfaces
{
    public interface IProductDomainRepository : IDomainRepository<Product>
    {
        Task<bool> Update(int id, Product entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default);
    }
}