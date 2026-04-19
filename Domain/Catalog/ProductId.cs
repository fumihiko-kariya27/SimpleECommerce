using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog
{
    // 商品IDを定義する
    public sealed class ProductId : IEquatable<ProductId>
    {
        public CategoryId Category { get; init; }
        public int Value { get; init; }

        internal static readonly int Min = 0;
        internal static readonly int Max = 9999;

        internal ProductId(CategoryId category, int id)
        {
            if (id < Min || Max < id)
            {
                throw new ArgumentException($"商品IDは{Min}から{Max}の間でなければなりません");
            }

            this.Category = category;
            this.Value = id;
        }

        internal string Code
        {
            get { return Category.ToCode() + "_" + Value.ToString().PadLeft(4, '0'); }
        }

        public override string ToString()
        {
            return this.Code;
        }

        public bool Equals(ProductId? other)
        {
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            return this.Category == other.Category && this.Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as ProductId);
        }

        public override int GetHashCode()
        {
            return this.Category.GetHashCode() ^ this.Value.GetHashCode();
        }
    }
}
