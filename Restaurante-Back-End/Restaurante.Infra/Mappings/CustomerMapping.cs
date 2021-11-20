using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurante.Domain.Users.Customers.Models;

namespace Restaurante.Infra.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Document)
                .IsRequired();

            builder
                .HasIndex(c => c.Document)
                .IsUnique();

            builder
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}
