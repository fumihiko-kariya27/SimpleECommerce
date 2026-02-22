using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog.Factory
{
    public class SimpleProductIdGenerator : IProductIdGenerator
    {
        public ProductId Generate(int productId, CategoryId categoryId)
        {
            return new ProductId(categoryId, productId);
        }
    }
}
