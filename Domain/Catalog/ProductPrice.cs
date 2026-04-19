namespace SimpleECommerce.Domain.Catalog
{
    public class ProductPrice : IComparable
    {
        public int Value { get; init; }

        private static readonly int MIN = 0;

        public ProductPrice(int price)
        {
            if (price < MIN)
            {
                throw new ArgumentException($"金額は{MIN}円以上である必要があります");
            }

            this.Value = price;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null)
            {
                return -1;
            }

            if (obj is ProductPrice other)
            {
                // 並べ替え順は既定で価格が安い順とする
                return this.Value - other.Value;
            }
            else
            {
                throw new ArgumentException("商品価格の比較対象が商品価格クラスでありません");
            }
        }
    }
}
