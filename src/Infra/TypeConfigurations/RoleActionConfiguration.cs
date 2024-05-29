using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class RoleActionConfiguration : BaseConfiguration<RoleAction>
    {
        public override void Configure(EntityTypeBuilder<RoleAction> builder)
        {
            base.Configure(builder);

            builder.HasOne(s => s.Role)
               .WithMany(g => g.RoleActions)
               .HasForeignKey(s => s.RoleId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}