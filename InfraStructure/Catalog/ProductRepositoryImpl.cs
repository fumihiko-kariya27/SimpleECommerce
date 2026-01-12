using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Models.Catalog;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Service.Catalog;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace SimpleECommerce.InfraStructure.Catalog
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly ECommerceDbContext context;

        public ProductRepositoryImpl(ECommerceDbContext context)
        { 
            this.context = context;
        }

        public async Task<(bool, Product?)> TrySelect(CategoryId category, int productId)
        {
            ProductModel? ret = await context.Products.Where(p => p.Category.Id == category && p.Id == productId).FirstOrDefaultAsync();
            return ret != null ? (true, ret.ToDomain()) : (false, null);
        }

        public async Task<IReadOnlyList<Product>> SelectAsync(Expression<Func<ProductModel, bool>>? predicate = null)
        {
            IQueryable<ProductModel> query = context.Products.AsQueryable();

            if (predicate != null) 
            {
                // 検索条件が指定されている場合のみ、指定条件でフィルタリングする
                query = query.Where(predicate);
            }
            query.Include(p => p.Images);
            List<ProductModel> products = await query.ToListAsync();
            List<Product> ret = new List<Product>();
            foreach (ProductModel product in products)
            {
                Product p = product.ToDomain();
                foreach (ProductImageModel im in product.Images)
                {
                    ProductImage image = new ProductImage(im.FileName, im.ContentType, im.ImageData);
                    p.Images.Add(image);
                }
                ret.Add(p);
            }
            return ret.ToImmutableList();
        }

        public async Task RegisterAsync(Product product)
        {
            ProductModel row = new();
            row.Id = product.Id.Id;
            row.CategoryId = product.Id.Category;
            row.Name = product.Name.Name;
            row.Description = product.Description.Desc;
            row.Price = product.Price.price;
            row.CreatedAt = DateTime.Now;
            row.UpdatedAt = DateTime.Now;

            await context.AddAsync(row);

            for (int i = 0; i < product.Images.Count; i++) 
            {
                ProductImageModel image = new();
                image.CategoryId = product.Id.Category;
                image.ProductId = product.Id.Id;
                image.ImageData = product.Images[i].Data;
                image.FileName = product.Images[i].FileName;
                image.ContentType = product.Images[i].ContentType;
                image.Sequence = i + 1;
                await context.AddAsync(image);
            }

            context.SaveChanges();
        }

        public async Task<(bool, ProductImage?)> SelectImageByPrimaryAsync(ProductId id, int sequence)
        { 
            ProductImageModel? model = await context.ProductImages.Where(i => 
                i.CategoryId == id.Category && 
                i.ProductId == id.Id && 
                i.Sequence == sequence
            ).SingleOrDefaultAsync();

            if (model == null) 
            {
                // 画像がなかった場合の処理は検討中
                return (false, null);
            }

            return (true, new ProductImage(model.FileName, model.ContentType, model.ImageData));
        }

        public async Task<(bool, Product?)> SelectByPrimayAsync(ProductId productId)
        {
            ProductModel? ret = await context.Products.Where(p => p.CategoryId == productId.Category && p.Id == productId.Id).SingleOrDefaultAsync();
            return ret != null ? (true, ret.ToDomain()) : (false, null);
        }
    }
}
