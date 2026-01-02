using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog
{
    public class Product
    {
        public ProductId Id { get; init; }
        public ProductName Name { get; init; }
        public Description Description { get; init; }
        public ProductPrice Price { get; init; }

        public Product(CategoryId category, int id, ProductName name, Description description, ProductPrice price)
        {
            if (name == null) 
            { 
                throw new ArgumentNullException("商品名にnullは設定できません");
            }

            if (description == null) 
            {
                throw new ArgumentNullException("商品説明にnullは設定できません");
            }

            if (price == null) 
            {
                throw new ArgumentNullException("商品価格にnullは設定できません");
            }

            this.Id = new ProductId(category, id);
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        public override string ToString()
        {
            return $"商品コード:{Id.Code} 商品名:{Name.Name}";
        }
    }
}
