using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Models;
using System;

namespace Restaurante.Domain.Products.Models
{
    public class Product : Entity<int>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int QuantityStock { get; private set; }
        public Photo Photo { get; private set; }
        public ProductCategory Category { get; private set; }
        public string Accompaniments { get; private set; }
        public decimal Price { get; private set; }
        public bool Available { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        private Product()
        {
        }

        public Product(string name, string description, decimal price, int quantiyStock, ProductCategory category, Photo photo, string accompaniments, bool available = false)
            : this(name, description, price, quantiyStock, category, accompaniments, available)
        {
            Photo = photo ?? throw new BasicTableException("Foto não pode ser nula!", NotificationKeys.InvalidEntity);
        }

        public Product(string name, string description, decimal price, int quantiyStock, ProductCategory category, string photoPath, string accompaniments, bool available = false)
            : this(name, description, price, quantiyStock, category, accompaniments, available)
        {
            Photo = new Photo(photoPath);
        }

        public Product(string name, string description, decimal price, int quantiyStock, ProductCategory category, string accompaniments, bool available = false)
            : this(name, description, price, quantiyStock, category, available)
        {
            Accompaniments = accompaniments;
        }

        public Product(string name, string description, decimal price, int quantityStock, ProductCategory category, bool available = false)
            : this(name, description, price, quantityStock, available)
        {
            Category = category ?? throw new BasicTableException("Categoria não pode ser nula!", NotificationKeys.InvalidEntity);
        }

        public Product(string name, string description, decimal price, int quantityStock, bool available = false)
            : this(name, description, price, available)
        {
            QuantityStock = quantityStock;
        }

        public Product(string name, string description, decimal price, bool available = false)
            : this()
        {
            ValidateNullString(name, "Nome do Produto");
            ValidateNullString(description, "Descrição do Produto");
            Name = name;
            Description = description;
            Available = available;
            Price = price;
        }

        public Product UpdateName(string name)
        {
            ValidateNullString(name, "Nome do produto");
            if (Name != name)
            {
                Name = name;
                UpdatedOn = DateTime.Now;
            }
            return this;
        }

        public Product UpdateDescription(string description)
        {
            ValidateNullString(description, "Descrição do produto");
            if (Description != description)
            {
                Description = description;
                UpdatedOn = DateTime.Now;
            }
            return this;
        }

        public Product AddQuantity(int quantity)
        {
            if (quantity > 0)
            {
                QuantityStock += quantity;
                UpdatedOn = DateTime.Now;
            }
            return this;
        }

        public Product RemoveQuantity(int quantity)
        {
            if (quantity > 0)
            {
                QuantityStock -= quantity;
                UpdatedOn = DateTime.Now;
            }
            return this;
        }

        public Product UpdatePhoto(Photo photo)
        {
            _ = photo ?? throw new BasicTableException("Foto não pode ser nula!", NotificationKeys.InvalidEntity);
            if (Photo?.Path != photo.Path)
            {
                Photo = photo;
                UpdatedOn = DateTime.Now;
            }
            return this;
        }

        public Product UpdatePhoto(string photo)
        {
            ValidateNullString(photo, "Caminho da foto");
            if (Photo?.Path != photo)
            {
                Photo.UpdatePath(photo);
                UpdatedOn = DateTime.Now;
            }
            return this;
        }

        public Product UpdatePrice(decimal price)
        {
            if (Price != price)
            {
                UpdatedOn = DateTime.Now;
                Price = price;
            }
            return this;
        }

        public Product UpdateAvailable(bool available)
        {
            if(Available != available)
            {
                UpdatedOn = DateTime.Now;
                Available = available;
            }
            return this;
        }

        public Product UpdateCategory(ProductCategory productCategory)
        {
            _ = productCategory ?? throw new BasicTableException("Categoria não pode ser nula!", NotificationKeys.InvalidEntity);
            if(Category?.Name != productCategory.Name)
            {
                UpdatedOn = DateTime.Now;
                Category = productCategory;
            }
            return this;
        }

        public Product UpdateAccompaniments(string accompaniments)
        {
            ValidateNullString(accompaniments, "Acompanhamentos");
            if(Accompaniments != accompaniments)
            {
                UpdatedOn = DateTime.Now;
                Accompaniments = accompaniments;
            }
            return this;
        }
    }
}
