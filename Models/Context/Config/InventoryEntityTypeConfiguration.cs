using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleECommerce.Models.Catalog;
using SimpleECommerce.Models.Stock;
using System.Reflection.Emit;

namespace SimpleECommerce.Models.Context.Config
{
    public class InventoryEntityTypeConfiguration : IEntityTypeConfiguration<InventoryModel>
    {
        public void Configure(EntityTypeBuilder<InventoryModel> builder)
        {
            builder.HasOne(i => i.Product).WithOne(p => p.Inventory)
                .HasForeignKey<InventoryModel>(i => new { i.Id, i.CategoryId })
                .HasPrincipalKey<ProductModel>(p => new { p.Id, p.CategoryId });
        }
    }
}
