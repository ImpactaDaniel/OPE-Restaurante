using System;
using System.Collections.Generic;

namespace Restaurante.Application.Common.Models
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? QuantityStock { get; set; }
        public PhotoResponseDTO Photo { get; set; }
        public ProductCategoryResponseDTO Category { get; set; }
        public string Accompaniments { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }

    public class PhotoResponseDTO
    {
        public string PhotoPath { get; set; }
    }

    public class ProductCategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductResponseDTO> Products { get; set; }
    }
}
