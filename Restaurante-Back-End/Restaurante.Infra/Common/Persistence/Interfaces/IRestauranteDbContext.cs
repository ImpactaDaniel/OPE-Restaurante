using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Products.Models;
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
    }
}