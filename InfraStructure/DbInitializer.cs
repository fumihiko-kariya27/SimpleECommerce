using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Models.Catalog;
using SimpleECommerce.Models.Context;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace SimpleECommerce.InfraStructure
{
    public static class DbInitializer
    {
        public static void Seed(ECommerceDbContext context)
        {
            context.Database.Migrate();

            CategorySeed(context);
            ProductSeed(context);
        }

        private static void CategorySeed(ECommerceDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            string json = File.ReadAllText("seed/category.json", Encoding.UTF8);
            List<CategoryModel> categories = JsonSerializer.Deserialize<List<CategoryModel>>(json)!;
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ProductSeed(ECommerceDbContext context)
        {
            if (context.Products.Any()) 
            {
                return;
            }

            string json = File.ReadAllText("seed/product.json", Encoding.UTF8);
            List<ProductModel> products = JsonSerializer.Deserialize<List<ProductModel>>(json)!;
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
