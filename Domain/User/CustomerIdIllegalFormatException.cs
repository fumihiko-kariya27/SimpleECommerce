using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Domain.User
{
    public class CustomerIdIllegalFormatException : DomainException
    {
        public CustomerIdIllegalFormatException(string message) : base(message)
        {
        }
    }
}
