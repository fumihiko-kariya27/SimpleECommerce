using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Service.Catalog
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> ListAsync();

        Task<bool> IsExistAsync(Product product);

        Task RegisterAsync(Product product);

        Task<bool> IsUniqueProduct(Product product);
    }
}
