using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class EmployeeRoleConfiguration : BaseConfiguration<EmployeeRole>
    {
        public override void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            base.Configure(builder);

            builder.HasOne(s => s.Role)
               .WithMany(g => g.EmployeeRoles)
               .HasForeignKey(s => s.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Employee)
               .WithMany(g => g.EmployeeRoles)
               .HasForeignKey(s => s.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}