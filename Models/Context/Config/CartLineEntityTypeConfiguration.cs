using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleECommerce.Models.Shopping;

namespace SimpleECommerce.Models.Context.Config
{
    public class CartLineEntityTypeConfiguration : IEntityTypeConfiguration<CartLineModel>
    {
        public void Configure(EntityTypeBuilder<CartLineModel> builder)
        {
            builder.HasOne(cl => cl.Product).WithMany()
                .HasForeignKey(cl => new { cl.ProductId, cl.Category })
                .HasPrincipalKey(p => new { p.Id, p.CategoryId });

            builder.HasOne(cl => cl.User).WithMany()
                .HasForeignKey(cl => cl.UserId)
                .HasPrincipalKey(u => new { u.Id });
        }
    }
}
