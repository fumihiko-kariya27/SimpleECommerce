using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog
{
    public class Product
    {
        public ProductId Id { get; init; }
        public ProductName Name { get; init; }
        public Description Description { get; init; }
        public ProductPrice Price { get; init; }
        public IList<ProductImage> Images { get; init; }

        public Product(CategoryId category, int id, ProductName name, Description description, ProductPrice price)
        {
            ArgumentNullException.ThrowIfNull(name, "商品名にnullは設定できません");
            ArgumentNullException.ThrowIfNull(description, "商品説明にnullは設定できません");
            ArgumentNullException.ThrowIfNull(price, "商品価格にnullは設定できません");

            this.Id = new ProductId(category, id);
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Images = Array.Empty<ProductImage>();
        }

        public override string ToString()
        {
            return $"商品コード:{Id.Code} 商品名:{Name.Name}";
        }
    }
}
