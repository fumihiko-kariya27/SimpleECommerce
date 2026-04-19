namespace SimpleECommerce.Domain.Purchase.Shopping
{
    internal class CartLinePrice : IEquatable<CartLinePrice>
    {
        internal int Value { get; init; }

        internal CartLinePrice(int price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("注文金額に負数は設定できません");
            }

            this.Value = price;
        }

        public bool Equals(CartLinePrice? other)
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

        public override bool Equals(object? obj) => Equals(obj as CartLinePrice);

        public override int GetHashCode() => this.Value.GetHashCode();
    }
}
