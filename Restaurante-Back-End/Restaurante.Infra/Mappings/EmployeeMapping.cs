using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurante.Domain.Users.Employees.Models;

namespace Restaurante.Infra.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder
                .HasKey(e => e.Id);

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.HasIndex(e => e.Document)
                .IsUnique();
        }
    }
}
