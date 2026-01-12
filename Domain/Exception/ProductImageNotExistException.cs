namespace SimpleECommerce.Domain.Exception
{
    public class ProductImageNotExistException : DomainException
    {
        public ProductImageNotExistException(string message) : base(message)
        {
        }
    }
}
