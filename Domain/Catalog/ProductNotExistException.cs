using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Domain.Catalog
{
    public class ProductNotExistException : DomainException
    {
        private readonly ProductId _productId;

        public ProductNotExistException(ProductId id) : base($"{id.Code}は存在しません")
        {
            _productId = id;
        }
    }
}
