using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Service.Catalog
{
    internal interface IProductService
    {
        internal IReadOnlyList<Product> List();
    }
}
