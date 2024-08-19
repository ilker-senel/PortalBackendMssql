using Infrastructure.Data.Entities;
using Infrastructure.Data.EntityFramework.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityFramework.Configurations
{
    public class ProductConfiguration : BaseConfiguration<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.ProductName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Description).HasMaxLength(200);

        }
    }
}
