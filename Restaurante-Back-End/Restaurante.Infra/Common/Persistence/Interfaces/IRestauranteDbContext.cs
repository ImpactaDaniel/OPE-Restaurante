using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Domain.Users.Employees.Models;

namespace Restaurante.Infra.Common.Persistence.Interfaces
{
    public interface IRestauranteDbContext : IDbContext
    {
        DbSet<Employee> Employees { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Phone> Phones { get; }
        DbSet<Address> Addresses { get; }
        DbSet<Bank> Banks { get; }
        DbSet<Product> Products { get; }
        DbSet<Photo> Photos { get; }
        DbSet<ProductCategory> ProductCategories { get; }
        DbSet<Invoice> Invoices { get; }
        DbSet<InvoiceLog> InvoiceLogs { get; }
        DbSet<InvoiceLine> InvoiceLines { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Payment> Payments { get; }
    }
}