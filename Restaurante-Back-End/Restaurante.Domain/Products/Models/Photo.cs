using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Products.Models
{
    public class Photo : Entity<int>
    {
        public string Path { get; private set; }
        private Photo()
        {
        }

        public Photo(string path)
            : this()
        {
            Path = path;
        }

        public Photo UpdatePath(string path)
        {
            ValidateNullString(path, "Caminho da foto");
            if (Path != path)
                Path = path;
            return this;
        }
    }
}
