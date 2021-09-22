namespace Restaurante.Infra.Common.Persistence
{
    internal class DatabaseInitializer : IInitializer
    {
        private readonly RestauranteDbContext _db;

        public DatabaseInitializer(RestauranteDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            _db.Database.EnsureCreated();
        }
    }
}
