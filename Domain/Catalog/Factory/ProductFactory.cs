using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog.Factory
{
    public class ProductFactory
    {
        private readonly IProductIdGenerator _idGenerator;

        public ProductFactory(IProductIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public Product Create(
            int Id,
            CategoryId category,
            string name,
            string description,
            int price,
            int quantity = 0
        )
        {
            ProductId productId = _idGenerator.Generate(Id, category);
            ProductName productName = new ProductName(name);
            Description productDesc = new Description(description);
            ProductPrice productPrice = new ProductPrice(price);

            return new Product(productId, productName, productDesc, productPrice, quantity);
        }
    }
}
