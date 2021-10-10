using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Products.Models
{
    public class ProductCategory : Entity<int>
    {
        public string Name { get; private set; }

        private ProductCategory()
        {
        }

        public ProductCategory(string name)
            : this()
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
