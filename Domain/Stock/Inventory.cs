using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Domain.Stock
{
    public class Inventory
    {
        // 商品ID
        private readonly ProductId _productId;
        // 在庫数
        private int _quantity;

        public Inventory(ProductId productId, int quantity = 0)
        {
            ArgumentNullException.ThrowIfNull(productId);

            _productId = productId;
            _quantity = quantity;
        }

        public int Quantity => _quantity;

        public int Increase(int addtional)
        {
            if (addtional < 0) 
            {
                throw new ArgumentException("追加在庫数に負数を指定することはできません");
            }

            _quantity += addtional;
            return _quantity;
        }

        public int Decrease(int reduced)
        {
            if (_quantity - reduced < 0)
            {
                throw new ArgumentException($"在庫数は{_quantity}のため、{reduced}個在庫を減らすことはできません");
            }

            _quantity -= reduced;
            return _quantity;
        }
    }
}
