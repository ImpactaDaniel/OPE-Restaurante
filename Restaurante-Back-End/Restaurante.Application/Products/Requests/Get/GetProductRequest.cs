using MediatR;
using Microsoft.Extensions.Logging;
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
    public class GetProductRequest : ProductRequest<GetProductRequest>, IRequest<Response<ProductResponseDTO>>
    {
        internal class GetProductRequestHandler : IRequestHandler<GetProductRequest, Response<ProductResponseDTO>>
        {
            private readonly IProductService _productService;
            private readonly IMapper<Product, ProductResponseDTO> _mapper;

            public GetProductRequestHandler(IProductService productService, IMapper<Product, ProductResponseDTO> mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<Response<ProductResponseDTO>> Handle(GetProductRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = await _productService.Get(request.Id, cancellationToken);
                    if (product is null)
                        return new Response<ProductResponseDTO>(false, null);
                    return new Response<ProductResponseDTO>(true, _mapper.Map(product));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
