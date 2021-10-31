using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Products.Common.Models;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Requests.Get
{
    public class SearchProductsRequest : ProductRequest<SearchProductsRequest>, IRequest<Response<PaginationInfo<ProductResponseDTO>>>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        internal class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, Response<PaginationInfo<ProductResponseDTO>>>
        {
            private readonly IProductService _productService;
            private readonly IMapper<Product, ProductResponseDTO> _mapper;

            public SearchProductsRequestHandler(IProductService productService, IMapper<Product, ProductResponseDTO> mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<Response<PaginationInfo<ProductResponseDTO>>> Handle(SearchProductsRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = await _productService.SearchProducts(GetSearchExpression(request), request.Page, request.Limit, cancellationToken);
                    if (products.Entities.Any())
                        return new Response<PaginationInfo<ProductResponseDTO>>(true, new PaginationInfo<ProductResponseDTO>
                        {
                            Entities = _mapper.Map(products.Entities),
                            Size = products.Size
                        });
                    return new Response<PaginationInfo<ProductResponseDTO>>(true, null);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static Expression<Func<Product, bool>> GetSearchExpression(SearchProductsRequest request)
            {
                return request.Field switch
                {
                    "name" => product => product.Name.Contains(request.Value),
                    "description" => product => product.Description.Contains(request.Value),
                    "availability" => product => product.Available == bool.Parse(request.Value),
                    "createdDate" => product => product.CreatedOn.Date == DateTime.Parse(request.Value).Date,
                    "category" => product => product.Category.Id == int.Parse(request.Value),
                    _ => throw new BasicTableException("Campo de filtro não disponível!", Domain.Common.Enums.NotificationKeys.Error),
                };
            }
        }
    }
}
