using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Service.Catalog
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> ListAsync();

        Task<bool> IsExistAsync(Product product);

        Task<bool> IsExistAsync(ProductId id);

        Task RegisterAsync(Product product, IFormFile? image);

        Task<bool> IsUniqueProduct(Product product);

        Task<Product> Get(ProductId productId);
    }
}
