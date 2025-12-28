using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Catalog;
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

        public async Task<IReadOnlyList<Product>> SelectAsync(Expression<Func<ProductModel, bool>>? predicate = null)
        {
            IQueryable<ProductModel> query = context.Products.AsQueryable();

            if (predicate != null) 
            {
                // 検索条件が指定されている場合のみ、指定条件でフィルタリングする
                query = query.Where(predicate);
            }

            List<ProductModel> products = await query.ToListAsync();
            List<Product> ret = new List<Product>();
            foreach (ProductModel product in products)
            {
                ret.Add(product.ToDomain());
            }
            return ret.ToImmutableList();
        }
    }
}
