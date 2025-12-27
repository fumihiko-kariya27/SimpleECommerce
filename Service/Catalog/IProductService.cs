using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Service.Catalog
{
    public interface IProductService
    {
        internal IReadOnlyList<Product> List();
    }
}
