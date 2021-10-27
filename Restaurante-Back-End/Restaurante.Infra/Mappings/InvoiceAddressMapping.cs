using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurante.Domain.Invoices.Models;

namespace Restaurante.Infra.Mappings
{
    public class InvoiceAddressMapping : IEntityTypeConfiguration<InvoiceAddress>
    {
        public void Configure(EntityTypeBuilder<InvoiceAddress> builder)
        {
            builder
                .ToTable("InvoiceAddresses")
                .HasKey(ia => ia.Id);
        }
    }
}
