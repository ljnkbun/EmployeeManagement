using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class UserRoleConfiguration : BaseConfiguration<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder);

            builder.HasOne(s => s.Role)
               .WithMany(g => g.UserRoles)
               .HasForeignKey(s => s.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.AppUser)
               .WithMany(g => g.UserRoles)
               .HasForeignKey(s => s.AppUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}