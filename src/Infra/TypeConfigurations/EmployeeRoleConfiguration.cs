using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class EmployeeRoleConfiguration : BaseConfiguration<EmployeeRole>
    {
        public override void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            base.Configure(builder);

        }
    }
}