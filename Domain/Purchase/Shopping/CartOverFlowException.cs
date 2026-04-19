using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Domain.Purchase.Shopping
{
    public class CartOverFlowException : DomainException
    {
        public CartOverFlowException(string message) : base(message)
        {
        }
    }
}
