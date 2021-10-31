using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Products.Common.Models;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Requests.Get
{
    public class GetAllProductsRequest : ProductRequest<GetAllProductsRequest>, IRequest<Response<PaginationInfo<ProductResponseDTO>>>
    {
        public int Limit { get; set; }
        public int Page { get; set; }
        internal class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, Response<PaginationInfo<ProductResponseDTO>>>
        {
            private readonly IProductService _productService;
            private readonly IMapper<Product, ProductResponseDTO> _mapper;

            public GetAllProductsRequestHandler(IProductService productService, IMapper<Product, ProductResponseDTO> mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<Response<PaginationInfo<ProductResponseDTO>>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = await _productService.GetAll(request.Page, request.Limit, cancellationToken);
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
        }
    }
}
