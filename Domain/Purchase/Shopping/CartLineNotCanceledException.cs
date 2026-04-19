using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Domain.Purchase.Shopping
{
    public class CartLineNotCanceledException : DomainException
    {
        public CartLineNotCanceledException(string message) : base(message)
        {
        }
    }
}
