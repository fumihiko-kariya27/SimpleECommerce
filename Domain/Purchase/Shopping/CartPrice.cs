namespace SimpleECommerce.Domain.Purchase.Shopping
{
    public class CartPrice : IEquatable<CartPrice>
    {
        public int Value { get; init; }

        public CartPrice(int value) 
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("注文金額に負数は設定できません");
            }

            this.Value = value;
        }

        public bool Equals(CartPrice? other)
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

        public override bool Equals(object? obj) => Equals(obj as CartPrice);

        public override int GetHashCode() => Value.GetHashCode();
    }
}
