using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Models.Catalog;

namespace SimpleECommerce.Models.Context
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }
    }
}
