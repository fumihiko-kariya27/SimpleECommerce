using Newtonsoft.Json.Linq;
using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Domain.Purchase.Shopping
{
    public class CartQuantity : IEquatable<CartQuantity>
    {
        private static readonly int MIN = 1;
        // 1商品当たりの同時購入数上限
        private static readonly int MAX = 10;

        public int Value { get; init; }

        public CartQuantity(int value)
        {
            if (value < MIN || MAX < value)
            { 
                throw new QuantityOutOfRangeException($"1商品当たりの注文可能個数は{MIN}個から{MAX}個までです");
            }

            this.Value = value;
        }

        public static CartQuantity ChangeQuantity(CartQuantity newQuantity)
        { 
            if (newQuantity.Value < MIN || MAX < newQuantity.Value)
            {
                throw new QuantityOutOfRangeException($"1商品当たりの注文可能個数は{MIN}個から{MAX}個までです");
            }

            return new CartQuantity(newQuantity.Value);
        }

        public bool Equals(CartQuantity? other)
        {
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is null || this.GetType() != other.GetType())
            {
                return false;
            }

            return this.Value == other.Value;
        }

        public override bool Equals(object? obj) => Equals(obj as CartQuantity);

        public override int GetHashCode() => Value.GetHashCode();

        public static CartQuantity operator +(CartQuantity c1, CartQuantity c2)
        {
            return new CartQuantity(c1.Value + c2.Value);
        }
    }
}
