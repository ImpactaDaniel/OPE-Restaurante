using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Products.Common.Models;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Requests.Get
{
    public class GetProductGroupByCategories : ProductRequest<GetProductGroupByCategories>, IRequest<Response<IEnumerable<ProductCategoryResponseDTO>>>
    {
        internal class GetProductGroupByCategoriesHandler : IRequestHandler<GetProductGroupByCategories, Response<IEnumerable<ProductCategoryResponseDTO>>>
        {
            private readonly IProductService _productService;
            private readonly IMapper<Product, ProductResponseDTO> _mapper;

            public GetProductGroupByCategoriesHandler(IProductService productService, IMapper<Product, ProductResponseDTO> mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<Response<IEnumerable<ProductCategoryResponseDTO>>> Handle(GetProductGroupByCategories request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = await _productService.GetProductsGroupByCategories(cancellationToken);
                    if (products.Any())
                        return new Response<IEnumerable<ProductCategoryResponseDTO>>(true, MapProductCategories(products));

                    return new Response<IEnumerable<ProductCategoryResponseDTO>>(true, null);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private IEnumerable<ProductCategoryResponseDTO> MapProductCategories(IEnumerable<ProductCategory> productCategories)
            {
                foreach (var category in productCategories)
                    yield return new ProductCategoryResponseDTO
                    {
                        Name = category.Name,
                        Products = _mapper.Map(category.Products)
                    };
            }
        }
    }
}
