using SimpleECommerce.Domain.Purchase.Shopping;
using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.Purchase.Shopping
{
    public interface ICartRepository
    {
        public Task<ShoppingCart> GetAsync(CustomerId id);

        public Task SaveAsync(CustomerId id, ShoppingCart cart);
    }
}
