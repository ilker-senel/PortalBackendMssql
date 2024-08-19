using Infrastructure.Data.Entities;
using Infrastructure.Data.EntityFramework.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityFramework.Configurations
{
    public class CategoryConfiguration : BaseConfiguration<Category, int>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.CategoryName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Description).HasMaxLength(200);


            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }

    }
}
