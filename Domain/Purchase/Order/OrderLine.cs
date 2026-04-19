using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Domain.Purchase.Order
{
    internal class OrderLine
    {
        internal ProductId ProductId { get; init; }
        internal ProductName Name { get; init; }
        internal ProductPrice Price { get; init; }
        internal OrderQuantity Quantity { get; init; }

        internal OrderLine(Product product, OrderQuantity quantity)
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(quantity);

            ProductId = product.Id;
            Name = new ProductName(product.Name.Value);
            Price = new ProductPrice(product.Price.Value);
            Quantity = quantity;
        }

        // 購入品一つ当たりの合計金額を取得する
        internal int SubTotal
        {
            get { return Price.Value * Quantity.Value; }
        }
    }
}
