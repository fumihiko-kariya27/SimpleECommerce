using Excp = System.Exception;

namespace SimpleECommerce.Domain.Exception
{
    public class DomainException : Excp
    {
        public DomainException(string message) : base(message) { }
    }
}
