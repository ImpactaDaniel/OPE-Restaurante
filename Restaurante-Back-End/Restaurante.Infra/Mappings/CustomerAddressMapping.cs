using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurante.Domain.Users.Customers.Models;

namespace Restaurante.Infra.Mappings
{
    public class CustomerAddressMapping : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder
                .ToTable("CustomerAddresses")
                .HasKey(ca => ca.Id);

            builder
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.Addresses);
        }
    }
}
