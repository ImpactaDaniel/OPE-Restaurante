using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Mappings;
using Restaurante.Infra.Seeds;

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

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceLog> InvoiceLogs { get; set; }

        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeMapping).Assembly);
            modelBuilder.ProductCategoriesSeed();
            modelBuilder.CustomersSeeds();
        }
    }
}
