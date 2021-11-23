using Restaurante.Domain.BasicEntities.Common.Interfaces;
using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Products.Models
{
    public class ProductCategory : Entity<int>, IBasicEntity
    {
        public string Name { get; private set; }

        private ProductCategory()
        {
        }

        public ProductCategory(int id)
            : base(id)
        {
        }

        public ProductCategory(string name)
        {
            ValidateNullString(name, "Nome da Categoria");
            Name = name;
        }

        public ProductCategory(int id, string name) :
            this(id)
        {
            ValidateNullString(name, "Nome da Categoria");
            Name = name;
        }

        public ProductCategory UpdateName(string name)
        {
            ValidateNullString(name, "Nome da Categoria");
            if (Name != name)
                Name = name;

            return this;
        }
    }
}
