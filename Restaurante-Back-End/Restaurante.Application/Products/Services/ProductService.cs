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
                var user = await GetEmployee(currentUserId, cancellationToken);

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
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar recuperar os produtos!");
            }
        }

        public async Task<Product> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await GetProduct(id, cancellationToken);
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

        public async Task<bool> UpdateProduct(int id, Product product, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await GetEmployee(currentUserId, cancellationToken);

                var currentProduct = await GetProduct(id, cancellationToken);

                currentProduct
                    .UpdateName(product.Name)
                    .UpdateDescription(product.Description)
                    .UpdateCategory(product.Category)
                    .UpdatePrice(product.Price)
                    .UpdateAvailable(product.Available)
                    .UpdatePhoto(product.Photo)
                    .AddQuantity(product.QuantityStock)
                    .UpdateAccompaniments(product.Accompaniments);

                currentProduct.UpdatedBy = currentUserId;

                return await _productDomainRepository.Update(currentProduct, cancellationToken);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar atualizar o produto!");
            }
        }

        public async Task<IEnumerable<Product>> SearchProducts(string name, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _productDomainRepository.Search(name, cancellationToken);
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

        public Task<bool> DeleteProduct(int id, int currentUserId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private async Task<Employee> GetEmployee(int currentUserId, CancellationToken cancellationToken = default)
        {
            var user = await _employeeRepository
                                     .Get(currentUserId, cancellationToken);

            if (user is null)
                throw new BasicTableException("Funcionário não encontrado!", Domain.Common.Enums.NotificationKeys.EntityNotFound);

            return user;
        }

        private async Task<Product> GetProduct(int id, CancellationToken cancellationToken = default)
        {
            var product = await _productDomainRepository.Get(p => p.Id == id, cancellationToken);
            if (product is null)
                throw new BasicTableException("Produto não encontrado!", Domain.Common.Enums.NotificationKeys.EntityNotFound);

            return product;
        }
    }
}
