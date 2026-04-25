using Microsoft.IdentityModel.Tokens;
using SimpleECommerce.Domain.Purchase.Shopping;
using SimpleECommerce.Domain.User;
using StackExchange.Redis;
using System.Text.Json;

namespace SimpleECommerce.InfraStructure.Purchase.Shopping
{
    internal class RedisCartRepository
    {
        private static readonly string keyFormat = "cache:cart:{0}";

        private readonly IDatabase db;

        internal RedisCartRepository(IConnectionMultiplexer redis)
        {
            ArgumentNullException.ThrowIfNull(redis);

            db = redis.GetDatabase();
        }

        internal async Task<ShoppingCart?> GetAsync(CustomerId id)
        {
            ArgumentNullException.ThrowIfNull(id);

            string key = string.Format(keyFormat, id);
            string? value = await db.StringGetAsync(key);

            if (value.IsNullOrEmpty())
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(value!)!;
        }

        internal async Task SaveAsync(CustomerId id, ShoppingCart cart)
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(cart);

            string key = string.Format(keyFormat, id);
            string json = JsonSerializer.Serialize<ShoppingCart>(cart);
            await db.StringSetAsync(key, json);
        }
    }
}
