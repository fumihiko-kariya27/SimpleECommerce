using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Domain.Exception
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
