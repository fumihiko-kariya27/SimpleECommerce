using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            var (exist, _) = await repository.TrySelect(id.Category, id.Value);
            return exist;
        }

        public async Task<IReadOnlyList<Product>> ListAsync()
        {
            return await repository.SelectAsync();
        }

        public async Task<bool> IsUniqueProduct(Product product)
        {
            IReadOnlyList<Product> ret = await repository.SelectAsync(p => p.Id == product.Id.Value && p.CategoryId == product.Id.Category);
            return ret.Count() > 0;
        }

        public async Task RegisterAsync(Product product)
        {
            if (await IsExistAsync(product)) 
            {
                // 登録商品が既に登録済みである場合は異常終了とする
                throw new ProductAlreadyExistException(product);
            }

            await repository.InsertAsync(product);
        }

        public async Task ModifyAsync(Product product)
        {
            await repository.UpDateAsync(product);
        }

        public async Task<ProductImage> GetImageAsync(ProductId id, int sequence)
        {
            var (exist, image) = await repository.SelectImageByPrimaryAsync(id, sequence);
            if (!exist) 
            {
                // 商品画像が存在しない場合は異常終了する
                throw new ProductImageNotExistException($"商品コード{id.Code}に{sequence}番の画像は存在しません");
            }
            return image!;
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

        public async Task Delete(ProductId productId)
        {
            await repository.DeleteByPrimaryAsync(productId);
        }
    }
}
