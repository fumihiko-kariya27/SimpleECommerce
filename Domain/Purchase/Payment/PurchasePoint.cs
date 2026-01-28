namespace SimpleECommerce.Domain.Purchase.Payment
{
    public class PurchasePoint
    {
        // ユーザーが日次で最初にログインした時に付与されるポイント
        public static readonly PurchasePoint LoginPointAtDay = new PurchasePoint(1000);

        public int Value { get; }

        internal PurchasePoint(int value = 0) 
        {
            if (value < 0)
            {
                throw new ArgumentException("購入ポイントに負数は設定できません");
            }

            Value = value;
        }

        internal PurchasePoint Plus(PurchasePoint other) => new PurchasePoint(Value + other.Value);

        internal PurchasePoint Minus(PurchasePoint other)
        {
            if (this.Value < other.Value) 
            {
                throw new ArithmeticException($"自身の保有ポイントは${this.Value}であるため、それより大きい{other.Value}を引くことはできません");
            }

            return new PurchasePoint(this.Value - other.Value);
        }
    }
}
