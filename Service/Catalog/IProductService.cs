using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Service.Catalog
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> ListAsync();

        Task<bool> IsExistAsync(Product product);

        Task<bool> IsExistAsync(ProductId id);

        Task RegisterAsync(Product product);

        Task ModifyAsync(Product product);

        Task<ProductImage> GetImageAsync(ProductId id, int sequence);

        Task<bool> IsUniqueProduct(Product product);

        Task<Product> GetAsync(ProductId productId);

        Task DeleteAsync(ProductId productId);
    }
}
