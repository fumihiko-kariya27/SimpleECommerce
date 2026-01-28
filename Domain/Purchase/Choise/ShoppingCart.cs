using AspNetCoreGeneratedDocument;
using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Domain.Purchase.Choise
{
    internal class ShoppingCart
    {
        // 一度にまとめて購入可能な商品数
        private readonly int PurchaseCapacityAtOnceTime = 10;

        private IList<Product> _products = [];

        internal int TotalPrice 
        {
            get { return this._products.Sum(p => p.Price.price);}        
        }

        internal int Count
        {
            get { return this._products.Count; }
        }

        internal bool Append(Product product)
        { 
            if (PurchaseCapacityAtOnceTime <= _products.Count)
            {
                // カートの空き容量がない場合は追加不可
                return false;
            }

            if(_products.Contains(product))
            {
                // 既にカートに追加済みの商品は追加不可
                return false;
            }

            _products.Add(product);
            return true;
        }

        internal void Remove(Product product)
        {
            _products.Remove(product);
        }

        internal void Clear()
        { 
            this._products.Clear();
        }
    }
}
