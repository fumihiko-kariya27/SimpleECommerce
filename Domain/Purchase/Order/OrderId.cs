using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Domain.Purchase.Order
{
    internal class OrderId : IEquatable<OrderId>
    {
        internal Guid Value { get; init; }

        internal OrderId() 
        {
            this.Value = Guid.NewGuid();
        }

        public bool Equals(OrderId? other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as OrderId);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(OrderId? id1, OrderId? id2)
        {
            if (Object.ReferenceEquals(id1, id2))
            { 
                return true;
            }

            if (id1 is null || id2 is null || id1.GetType() != id2.GetType())
            {
                return false;
            }

            return id1.Value == id2.Value;
        }

        public static bool operator !=(OrderId? id1, OrderId? id2)
        {
            return !(id1 == id2);
        }
    }
}
