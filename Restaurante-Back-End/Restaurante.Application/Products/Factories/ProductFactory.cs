using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Products.Factories.Interfaces;
using Restaurante.Domain.Products.Models;

namespace Restaurante.Application.Products.Factories
{
    internal class ProductFactory : IProductFactory
    {
        private bool _descriptionSetted, _nameSetted, _photoSetted, _photoPathSetted, _categorySetted, _accompanimentsSetted, _quantitySetted, _priceSetted, _available;
        private string _description, _name, _photoPath, _accompaniments;
        private Photo _photo;
        private decimal _price;
        private ProductCategory _category;
        private int _quantity;

        public Product Build()
        {
            Validate();
            if (_photoSetted)
                return new Product(_name, _description, _price, _quantity, _category, _photo, _accompaniments, _available);
            if (_photoPathSetted)
                return new Product(_name, _description, _price, _quantity, _category, _photoPath, _accompaniments, _available);
            if (_accompanimentsSetted)
                return new Product(_name, _description, _price, _quantity, _category, _accompaniments, _available);
            if (_categorySetted)
                return new Product(_name, _description, _price, _quantity, _category, _available);
            if (_quantitySetted)
                return new Product(_name, _description, _price, _quantity, _available);

            return new Product(_name, _description, _price, _available);

        }

        public IProductFactory WithAccompaniments(string accompaniments)
        {
            _accompanimentsSetted = true;
            _accompaniments = accompaniments;
            return this;
        }

        public IProductFactory WithAvailability(bool available)
        {
            _available = available;
            return this;
        }

        public IProductFactory WithCategory(ProductCategory category)
        {
            _categorySetted = true;
            _category = category;
            return this;
        }

        public IProductFactory WithDescription(string description)
        {
            _descriptionSetted = true;
            _description = description;
            return this;
        }

        public IProductFactory WithName(string name)
        {
            _nameSetted = true;
            _name = name;
            return this;
        }

        public IProductFactory WithPhoto(Photo photo)
        {
            _photoSetted = true;
            _photo = photo;
            return this;
        }

        public IProductFactory WithPhoto(string photoPath)
        {
            _photoPathSetted = true;
            _photoPath = photoPath;
            return this;
        }

        public IProductFactory WithPrice(decimal price)
        {
            _priceSetted = true;
            _price = price;
            return this;
        }

        public IProductFactory WithQuantity(int quantity)
        {
            _quantitySetted = true;
            _quantity = quantity;
            return this;
        }

        private void Validate()
        {
            if (!_nameSetted)
                throw new BasicTableException("Nome do produto precisa ser informado!", NotificationKeys.InvalidEntity);
            if (!_descriptionSetted)
                throw new BasicTableException("Descrição do produto precisa ser informada!", NotificationKeys.InvalidEntity);
            if (!_priceSetted)
                throw new BasicTableException("Preço do produto precisa ser informado!", NotificationKeys.InvalidEntity);
        }
    }
}
