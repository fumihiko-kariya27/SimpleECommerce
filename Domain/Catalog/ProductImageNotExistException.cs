using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Domain.Catalog
{
    public class ProductImageNotExistException : DomainException
    {
        public ProductImageNotExistException(string message) : base(message)
        {
        }
    }
}
