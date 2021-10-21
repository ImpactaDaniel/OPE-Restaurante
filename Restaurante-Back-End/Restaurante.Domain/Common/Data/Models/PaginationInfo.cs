using System.Collections.Generic;

namespace Restaurante.Domain.Common.Data.Models
{
    public class PaginationInfo<TEntity>
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public int Size { get; set; }
    }
}
