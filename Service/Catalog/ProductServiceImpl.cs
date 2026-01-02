using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Exception;

namespace SimpleECommerce.Service.Catalog
{
    internal class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository repository;

        public ProductServiceImpl(IProductRepository repository) 
        { 
            this.repository = repository;
        }

        public async Task<bool> IsExistAsync(Product product)
        {
            var (exist, _) = await repository.TrySelect(product.Id.Category, product.Id.Id);
            return exist;
        }

        public async Task<IReadOnlyList<Product>> ListAsync()
        {
            return await repository.SelectAsync();
        }

        public async Task<bool> IsUniqueProduct(Product product)
        {
            IReadOnlyList<Product> ret = await repository.SelectAsync(p => p.Id == product.Id.Id && p.CategoryId == product.Id.Category);
            return ret.Count() > 0;
        }

        public async Task RegisterAsync(Product product)
        {
            if (await IsExistAsync(product)) 
            {
                // 登録商品が既に登録済みである場合は異常終了とする
                throw new ProductAlreadyExistException(product);
            }

            await repository.RegisterAsync(product);
        }
    }
}
