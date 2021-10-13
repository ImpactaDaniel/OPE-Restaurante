using Microsoft.Extensions.Logging;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDomainRepository _productDomainRepository;
        private readonly IEmployeeDomainRepository<Employee> _employeeRepository;
        private readonly INotifier _notifier;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductDomainRepository productDomainRepository, INotifier notifier, ILogger<ProductService> logger, IEmployeeDomainRepository<Employee> employeeRepository)
        {
            _productDomainRepository = productDomainRepository;
            _notifier = notifier;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> CreateProduct(Product product, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _employeeRepository
                                    .Get(currentUserId, cancellationToken);

                if (user is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound("Funcionário"));
                    return false;
                }

                product.CreatedBy = currentUserId;
                product.CreatedOn = DateTime.Now;

                return await _productDomainRepository.Save(product, cancellationToken);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar criar o produto!");
            }
        }

        public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _productDomainRepository.GetAll(cancellationToken);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar recuperar os produtos!");
            }
        }

        public async Task<Product> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _productDomainRepository.Get(p => p.Id == id, cancellationToken);
                if (product is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound("Produto"));
                    return null;
                }
                return product;
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar recuperar os produtos!");
            }
        }

        public Task<bool> UpdateProduct(int id, Product product, int currentUserId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
