using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Models.Catalog;
using System.Linq.Expressions;

namespace SimpleECommerce.Service.Catalog
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> SelectAsync(Expression<Func<ProductModel, bool>>? predicate = null);

        Task<(bool success, Product? product)> TrySelect(CategoryId category, int productId);

        Task RegisterAsync(Product product);
    }
}
