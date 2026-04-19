using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Domain.Purchase.Shopping
{
    internal class CartLine : IEquatable<CartLine>
    {
        public CartLineId Id { get; init; }
        internal ProductId ProductId { get; init; }
        internal ProductName Name { get; init; }
        internal ProductPrice Price { get; init; }
        public CartQuantity Quantity { get; private set; }

        internal CartLine(Product product, int quantity)
        { 
            this.Id = new CartLineId();
            this.ProductId = product.Id;
            this.Name = new ProductName(product.Name.Value);
            this.Price = new ProductPrice(product.Price.Value);
            this.Quantity = new CartQuantity(quantity);
        }

        internal CartLinePrice TotalPrice
        {
            get { return new CartLinePrice(Price.Value * Quantity.Value); }
        }

        internal void ChangeQuantity(int newValue)
        {
            this.Quantity = CartQuantity.ChangeQuantity(newValue);
        }

        public bool Equals(CartLine? other) => this == other;

        public override bool Equals(object? obj) => Equals(obj as CartLine);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(CartLine? c1, CartLine? c2)
        {
            if (Object.ReferenceEquals(c1, c2))
            {
                return true;
            }

            if (c1 is null || c2 is null || c1.GetType() != c2.GetType())
            {
                return false;
            }

            return c1.Id.Equals(c2.Id);
        }

        public static bool operator !=(CartLine? c1, CartLine? c2)
        {
            return !(c1 == c2);
        }
    }
}
