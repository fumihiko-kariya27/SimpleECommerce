namespace SimpleECommerce.Domain.Purchase.Order
{
    internal class OrderQuantity
    {
        private static readonly int MIN = 1;
        private static readonly int MAX = 99;

        internal int Value { get; init; }

        internal OrderQuantity(int value)
        {
            if (value < MIN || MAX < value)
            { 
                throw new ArgumentOutOfRangeException($"注文数は{MIN}個から{MAX}個の範囲でなければなりません [注文数 = {value}]");
            }

            Value = value;
        }
    }
}
