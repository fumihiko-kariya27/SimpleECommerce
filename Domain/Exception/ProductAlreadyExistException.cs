using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Domain.Exception
{
    /// <summary>
    /// 登録商品が既に登録済みである場合に投げられる
    /// </summary>
    public class ProductAlreadyExistException : DomainException
    {
        private readonly Product _product;

        public ProductAlreadyExistException(Product product) : base($"{product.ToString()}は既に登録済みの商品です") 
        { 
            _product = product;
        }
    }
}
