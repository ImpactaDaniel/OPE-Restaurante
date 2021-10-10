using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Mappings;

namespace Restaurante.Infra.Common.Persistence
{
    internal class RestauranteDbContext : DbContext, IRestauranteDbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeMapping).Assembly);
        }
    }
}
