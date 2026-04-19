using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Domain.Stock;

namespace SimpleECommerce.Domain.Catalog
{
    public class Product : IEquatable<Product>
    {
        public ProductId Id { get; init; }
        public ProductName Name { get; init; }
        public Description Description { get; init; }
        public ProductPrice Price { get; init; }
        public IList<ProductImage> Images { get; init; }
        public Inventory Inventory { get; init; }

        public Product(ProductId id, ProductName name, Description description, ProductPrice price, int quantity = 0)
        {
            ArgumentNullException.ThrowIfNull(id, "商品IDにnullは設定できません");
            ArgumentNullException.ThrowIfNull(name, "商品名にnullは設定できません");
            ArgumentNullException.ThrowIfNull(description, "商品説明にnullは設定できません");
            ArgumentNullException.ThrowIfNull(price, "商品価格にnullは設定できません");

            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Images = new List<ProductImage>();
            this.Inventory = new Inventory(this.Id, quantity);
        }

        public override string ToString()
        {
            return $"商品コード:{Id.Code} 商品名:{Name.Value}";
        }

        public bool Equals(Product? other)
        {
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as Product);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
