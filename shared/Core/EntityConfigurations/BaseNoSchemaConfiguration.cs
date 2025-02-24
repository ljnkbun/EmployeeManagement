using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.EntityConfigurations
{
    public class BaseNoSchemaConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {

            builder.Property(e => e.CreatedDate)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_DATE");

            builder.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_DATE");
        }
    }
}
