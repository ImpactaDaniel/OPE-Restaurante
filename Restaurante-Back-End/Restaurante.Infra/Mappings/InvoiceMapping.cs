using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurante.Domain.Invoices.Models;

namespace Restaurante.Infra.Mappings
{
    public class InvoiceMapping : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .HasOne(i => i.Address);

            builder
                .HasOne(i => i.Payment);
        }
    }
}
