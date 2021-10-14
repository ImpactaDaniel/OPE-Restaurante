using Restaurante.Application.Common;

namespace Restaurante.Application.Products.Common.Models
{
    public class ProductRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public int CurrentUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityStock { get; set; }
        public ProductCategoryRequest Category { get; set; }
        public string Accompaniments { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
    }

    public class ProductCategoryRequest
    {
        public int Id { get; set; }
    }

    public class PhotoRequest
    {
        public string PhotoB64 { get; set; }
        public string FileName { get; set; }
    }
}
