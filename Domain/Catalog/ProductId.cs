using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog
{
    // 商品IDを定義する
    public sealed class ProductId
    {
        private readonly CategoryId category;
        private readonly int id;

        private readonly int Min = 0;
        private readonly int Max = 9999;

        internal ProductId(CategoryId category, int id)
        {
            if (id < Min || Max < id)
            {
                throw new ArgumentException($"商品IDは{Min}から{Max}の間でなければなりません");
            }

            this.category = category;
            this.id = id;
        }

        internal string Id
        {
            get { return category.ToCode() + "_" + id.ToString().PadLeft(4, '0'); }
        }
    }
}
