using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Domain.Catalog.Factory
{
    public interface IProductIdGenerator
    {
        ProductId Generate(int productId, CategoryId categoryId);
    }
}
