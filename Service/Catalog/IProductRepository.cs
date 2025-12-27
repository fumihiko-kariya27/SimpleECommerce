using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Models.Catalog;
using System.Linq.Expressions;

namespace SimpleECommerce.Service.Catalog
{
    internal interface IProductRepository
    {
        internal IReadOnlyList<Product> Select(Expression<Func<ProductModel, bool>>? predicate = null);
    }
}
