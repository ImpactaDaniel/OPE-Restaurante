using Restaurante.Application.Common;

namespace Restaurante.Application.Products.Common.Models
{
    public class ProductRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityStock { get; set; }
        public PhotoRequest Photo { get; set; }
        public ProductCategoryRequest Category { get; set; }
        public string Accompaniments { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
    }

    public class PhotoRequest
    {
        public string PhotoPath { get; set; }
    }

    public class ProductCategoryRequest
    {
        public string Name { get; set; }
    }
}
