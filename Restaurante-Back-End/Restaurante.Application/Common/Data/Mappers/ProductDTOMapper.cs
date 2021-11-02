using Restaurante.Application.Common.Models;
using Restaurante.Domain.Products.Models;

namespace Restaurante.Application.Common.Data.Mappers
{
    public class ProductDTOMapper : Mapper<Product, ProductResponseDTO>
    {
        public override ProductResponseDTO Map(Product source) =>
            new()
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Category = new ProductCategoryResponseDTO
                {
                    Name = source.Category.Name,
                    Id = source.Category.Id
                },
                Accompaniments = source.Accompaniments,
                Available = source.Available,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                Photo = new PhotoResponseDTO
                {
                    PhotoPath = "Products/" + source.Photo?.Path
                },
                Price = source.Price,
                QuantityStock = source.QuantityStock,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn
            };
    }
}
