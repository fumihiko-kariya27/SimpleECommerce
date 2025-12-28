using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Service.Catalog
{
    internal class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository repository;

        public ProductServiceImpl(IProductRepository repository) 
        { 
            this.repository = repository;
        }

        public async Task<IReadOnlyList<Product>> ListAsync()
        {
            return await repository.SelectAsync();
        }
    }
}
