using Core.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TypeConfigurations
{
    public class TestConfiguration : BaseNoSchemaConfiguration<Test>
    {
        public override void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable(typeof(Test).Name, "ABCD").HasKey(m => m.Id);
            builder.Property(e => e.Name).HasMaxLength(500);
            builder.HasIndex(e => e.Code).IsUnique();
            base.Configure(builder);

        }
    }
}