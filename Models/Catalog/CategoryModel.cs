using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Models.Catalog;

namespace SimpleECommerce.Models.Catalog
{
    public class CategoryModel
    {
        public CategoryId Id { get; set; } = CategoryId.None;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<ProductModel> Products { get; } = new List<ProductModel>();
    }
}
