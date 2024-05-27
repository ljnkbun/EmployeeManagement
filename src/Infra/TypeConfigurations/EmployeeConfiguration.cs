using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class EmployeeConfiguration : BaseConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).HasMaxLength(500);
            builder.Property(e => e.Username).HasMaxLength(50).IsRequired();
            builder.HasIndex(e => e.Username).IsUnique();
            builder.Property(e => e.Password).HasMaxLength(50).IsRequired();

            builder.HasOne(s => s.Division)
               .WithMany(g => g.Employees)
               .HasForeignKey(s => s.DivisionId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}