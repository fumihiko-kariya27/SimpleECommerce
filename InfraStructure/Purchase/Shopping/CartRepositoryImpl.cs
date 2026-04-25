using SimpleECommerce.Domain.Purchase.Shopping;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Service.Purchase.Shopping;
using StackExchange.Redis;

namespace SimpleECommerce.InfraStructure.Purchase.Shopping
{
    internal class CartRepositoryImpl : ICartRepository
    {
        private readonly DbCartRepository db;
        private readonly RedisCartRepository cache;

        public CartRepositoryImpl(ECommerceDbContext context, IConnectionMultiplexer redis)
        { 
            db = new (context);
            cache = new (redis);
        }

        public async Task<ShoppingCart> GetAsync(CustomerId id)
        {
            ShoppingCart? cart = await cache.GetAsync(id);
            if (cart is null)
            {
                ShoppingCart newCart = await db.GetAsync(id);
                await cache.SaveAsync(id, newCart);
                return newCart;
            }
            return cart;
        }

        public async Task SaveAsync(CustomerId id, ShoppingCart cart)
        {
            await cache.SaveAsync(id, cart);
            await db.SaveAsync(id, cart);
        }
    }
}
