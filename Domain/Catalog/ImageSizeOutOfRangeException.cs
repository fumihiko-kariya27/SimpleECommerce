using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Domain.Catalog
{
    public class ImageSizeOutOfRangeException : DomainException
    {
        public ImageSizeOutOfRangeException(string message) : base(message)
        {
        }
    }
}
