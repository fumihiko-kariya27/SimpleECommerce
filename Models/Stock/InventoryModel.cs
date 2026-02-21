using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Models.Catalog;

namespace SimpleECommerce.Models.Stock
{
    [PrimaryKey(nameof(Id), nameof(CategoryId))]
    public class InventoryModel
    {
        public int Id { get; set; }

        public CategoryId CategoryId { get; set; }

        public ProductModel Product { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
