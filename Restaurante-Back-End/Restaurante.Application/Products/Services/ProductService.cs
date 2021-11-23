using Microsoft.Extensions.Logging;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDomainRepository _productDomainRepository;
        private readonly IEmployeeDomainRepository<Employee> _employeeRepository;
        private readonly IBasicEntitiesService _basicEntitiesService;
        private readonly INotifier _notifier;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductDomainRepository productDomainRepository,
            INotifier notifier,
            ILogger<ProductService> logger,
            IEmployeeDomainRepository<Employee> employeeRepository,
            IBasicEntitiesService basicEntitiesService)
        {
            _productDomainRepository = productDomainRepository;
            _notifier = notifier;
            _logger = logger;
            _employeeRepository = employeeRepository;
            _basicEntitiesService = basicEntitiesService;
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
            catch (RestauranteException e)
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

        public async Task<PaginationInfo<Product>> GetAll(int page, int length = 20, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _productDomainRepository.GetAll(page * length, length, cancellationToken);
            }
            catch (RestauranteException e)
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
            catch (RestauranteException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar recuperar o produto!");
            }
        }

        public async Task<bool> UpdateProduct(int id, Product product, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await GetEmployee(currentUserId, cancellationToken);

                var currentProduct = await GetProduct(id, cancellationToken);

                await DeletePhoto(currentProduct.Photo, cancellationToken);
                
                currentProduct
                    .UpdateName(product.Name)
                    .UpdateDescription(product.Description)
                    .UpdateCategory(product.Category)
                    .UpdatePrice(product.Price)
                    .UpdateAvailable(product.Available)
                    .UpdatePhoto(product.Photo)
                    .UpdateQuantity(product.QuantityStock)
                    .UpdateAccompaniments(product.Accompaniments);

                currentProduct.UpdatedBy = currentUserId;

                return await _productDomainRepository.Update(currentProduct, cancellationToken);
            }
            catch (RestauranteException e)
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

        public async Task<PaginationInfo<Product>> SearchProducts(Expression<Func<Product, bool>> condition, int page, int limit, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _productDomainRepository.Search(condition, page, limit, cancellationToken);
                return product;
            }
            catch (RestauranteException e)
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

        public async Task<bool> DeleteProduct(int id, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await GetEmployee(currentUserId, cancellationToken);

                var currentProduct = await GetProduct(id, cancellationToken);

                await DeletePhoto(currentProduct.Photo, cancellationToken);

                return await _productDomainRepository.Delete(currentProduct, cancellationToken);
            }
            catch (RestauranteException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar remover o produto!");
            }
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

        private async Task DeletePhoto(Photo photo, CancellationToken cancellationToken = default)
        {
            if (photo is null)
                return;

            var photoDeleted = await _basicEntitiesService.DeleteEntity(photo, cancellationToken);

            if (!photoDeleted)
                throw new BasicTableException("Houve um erro ao tentar atualizar o produto!", Domain.Common.Enums.NotificationKeys.Error);
        }

        public async Task<IEnumerable<ProductCategory>> GetProductsGroupByCategories(CancellationToken cancellationToken = default) =>
            await _productDomainRepository.GetProductsGroupByCategories(cancellationToken);
    }
}
