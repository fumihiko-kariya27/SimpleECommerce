using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Domain.Exception;
using SimpleECommerce.Service.Image;

namespace SimpleECommerce.Service.Catalog
{
    internal class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IImageStorage imageStorage;

        // 商品画像のサイズの上限(単位：byte)
        private readonly int maxImageSize = 1024 * 1024;

        public ProductServiceImpl(IProductRepository repository, IImageStorage imageStorage) 
        { 
            this.repository = repository;
            this.imageStorage = imageStorage;
        }

        public async Task<bool> IsExistAsync(Product product)
        {
            return await IsExistAsync(product.Id);
        }

        public async Task<bool> IsExistAsync(ProductId id)
        {
            var (exist, _) = await repository.TrySelect(id.Category, id.Id);
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

        public async Task RegisterAsync(Product product, IFormFile? image)
        {
            if (image != null && image.Length > maxImageSize) 
            {
                throw new ImageSizeOutOfRangeException($"商品画像は${maxImageSize}バイト以内で設定してください");
            }

            if (await IsExistAsync(product)) 
            {
                // 登録商品が既に登録済みである場合は異常終了とする
                throw new ProductAlreadyExistException(product);
            }

            await repository.RegisterAsync(product);
            if (image != null)
            {
                string uri = $"product/{((int)(product.Id.Category)).ToString("D2")}/{product.Id.Id.ToString("D4")}.jpg";

                Uri path = await imageStorage.SaveAsync(image, uri);
                product.Image = new ProductImage(path);
            }
        }

        public async Task<Product> Get(ProductId productId)
        {
            var (exist, product) = await repository.SelectByPrimayAsync(productId);
            if (!exist)
            {
                // 検索商品が存在しない場合は異常終了とする
                throw new ProductNotExistException(productId);
            }

            return product!;
        }
    }
}
