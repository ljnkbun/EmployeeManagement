using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).HasMaxLength(500);
            builder.Property(e => e.Username).HasMaxLength(50).IsRequired();
            builder.HasIndex(e => e.Username).IsUnique();
            builder.Property(e => e.Password).HasMaxLength(5000).IsRequired();

        }
    }
}