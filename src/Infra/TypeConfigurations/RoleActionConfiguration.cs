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

            builder.Property(e => e.Controller).HasMaxLength(50);
            builder.Property(e => e.Action).HasMaxLength(50).IsRequired();
            builder.Property(e => e.ControllerActionId).HasMaxLength(50).HasDefaultValue(null);
            builder.Property(e => e.RoleId).HasMaxLength(50).HasDefaultValue(null);

            builder.HasOne(s => s.Role)
               .WithMany(g => g.RoleActions)
               .HasForeignKey(s => s.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ControllerAction)
               .WithMany(g => g.RoleActions)
               .HasForeignKey(s => s.ControllerActionId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}