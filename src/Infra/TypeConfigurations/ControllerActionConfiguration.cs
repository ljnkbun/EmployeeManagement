using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class ControllerActionConfiguration : BaseConfiguration<ControllerAction>
    {
        public override void Configure(EntityTypeBuilder<ControllerAction> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).HasMaxLength(500);
            builder.Property(e => e.Code).HasMaxLength(100);
            builder.Property(e => e.Controller).HasMaxLength(100);
            builder.Property(e => e.Action).HasMaxLength(100);

        }
    }
}