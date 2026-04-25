using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Factory;
using SimpleECommerce.Domain.Purchase.Shopping;
using SimpleECommerce.Domain.Stock;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Catalog;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.Shopping;

namespace SimpleECommerce.InfraStructure.Purchase.Shopping
{
    internal class DbCartRepository
    {
        private readonly ECommerceDbContext context;

        internal DbCartRepository(ECommerceDbContext context)
        { 
            this.context = context;
        }

        internal async Task<ShoppingCart> GetAsync(CustomerId id)
        {
            ArgumentNullException.ThrowIfNull(id);

            ShoppingCart cart = new();

            List<CartLineModel> cartLines = await context.CartLines
                .Include(cl => cl.Product)
                .Include(cl => cl.User)
                .Where(cl => cl.UserId == id.Value).ToListAsync();
            foreach (var cartLine in cartLines)
            {
                Product product = Reconstruct(cartLine);
                CartLine line = new (product, cartLine.Quantity);
                cart.Add(line);
            }

            return cart;
        }

        internal async Task SaveAsync(CustomerId id, ShoppingCart cart)
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(cart);

            using var tx = context.Database.BeginTransaction();
            try
            {
                // カートを保存する時は既存データとの総入れ替えとする
                await context.CartLines.Where(cl => cl.UserId == id.Value).ExecuteDeleteAsync();

                DateTime now = DateTime.Now;
                foreach (var cartLine in cart.Contents)
                {
                    CartLineModel row = ToEntity(id, cartLine);
                    row.CreatedAt = now;
                    row.UpdatedAt = now;
                    await context.CartLines.AddAsync(row);
                }

                await context.SaveChangesAsync();
                tx.Commit();
            }
            catch
            {
                // ロールバックはEF Core側で自動的に実行されるため記載不要
                throw;
            }
        }

        // カート明細より商品ドメインを復元する
        private static Product Reconstruct(CartLineModel cartLine)
        {
            ProductModel product = cartLine.Product!;
            ProductId id = new (product.CategoryId, product.Id);
            ProductName name = new (product.Name);
            Description description = new (product.Description);
            ProductPrice price = new (product.Price);
            return new(id, name, description, price, product.Inventory.Quantity);
        }

        private static CartLineModel ToEntity(CustomerId id, CartLine cartLine)
        {
            CartLineModel ret = new CartLineModel();
            ret.Id = cartLine.Id.ToString();
            ret.UserId = id.Value;
            ret.ProductId = cartLine.ProductId.Value;
            ret.Category = cartLine.ProductId.Category;
            ret.ProductName = cartLine.Name.Value;
            ret.ProductPrice = cartLine.Price.Value;
            ret.Quantity = cartLine.Quantity.Value;
            return ret;
        }
    }
}
