namespace SimpleECommerce.Domain.Purchase.Shopping
{
    internal class CartLineId : IEquatable<CartLineId>
    {
        private readonly Guid Value;

        internal CartLineId() 
        { 
            Value = Guid.NewGuid();
        }

        public override bool Equals(object? obj) => Equals(obj as CartLineId);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(CartLineId? other) => this == other;

        public static bool operator ==(CartLineId? id1, CartLineId? id2)
        {
            if (Object.ReferenceEquals(id1, id2))
            {
                return true;
            }

            if (id1 is null || id2 is null || id1.GetType() != id2.GetType()) 
            {
                return false;
            }

            return id1.Value.Equals(id2.Value);
        }

        public static bool operator !=(CartLineId? id1, CartLineId? id2)
        {
            return !(id1 == id2);
        }
    }
}
