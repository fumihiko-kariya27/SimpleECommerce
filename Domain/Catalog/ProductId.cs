using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog
{
    // 商品IDを定義する
    public sealed class ProductId
    {
        public CategoryId Category { get; init; }
        public int Id { get; init; }

        internal static readonly int Min = 0;
        internal static readonly int Max = 9999;

        internal ProductId(CategoryId category, int id)
        {
            if (id < Min || Max < id)
            {
                throw new ArgumentException($"商品IDは{Min}から{Max}の間でなければなりません");
            }

            this.Category = category;
            this.Id = id;
        }

        internal string Code
        {
            get { return Category.ToCode() + "_" + Id.ToString().PadLeft(4, '0'); }
        }

        public override string ToString()
        {
            return this.Code;
        }
    }
}
