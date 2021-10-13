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
    public class SearchProductsRequest : ProductRequest<SearchProductsRequest>, IRequest<Response<IEnumerable<ProductResponseDTO>>>
    {
        internal class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, Response<IEnumerable<ProductResponseDTO>>>
        {
            private readonly IProductService _productService;
            private readonly IMapper<Product, ProductResponseDTO> _mapper;

            public SearchProductsRequestHandler(IProductService productService, IMapper<Product, ProductResponseDTO> mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<Response<IEnumerable<ProductResponseDTO>>> Handle(SearchProductsRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = await _productService.SearchProducts(request.Name, cancellationToken);
                    if (products.Any())
                        return new Response<IEnumerable<ProductResponseDTO>>(true, Map(products));
                    return new Response<IEnumerable<ProductResponseDTO>>(true, null);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private IEnumerable<ProductResponseDTO> Map(IEnumerable<Product> products)
            {
                foreach (var product in products)
                    yield return _mapper.Map(product);
            }
        }
    }
}
