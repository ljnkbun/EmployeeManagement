using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class DivisionConfiguration : BaseConfiguration<Division>
    {
        public override void Configure(EntityTypeBuilder<Division> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).HasMaxLength(500);

        }
    }
}