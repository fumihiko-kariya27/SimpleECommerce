using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Service.Catalog
{
    internal class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository repository;

        internal ProductServiceImpl(IProductRepository repository) 
        { 
            this.repository = repository;
        }

        public IReadOnlyList<Product> List()
        {
            return repository.Select();
        }
    }
}
