using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Models.Catalog;
using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Models.Stock
{
    [PrimaryKey(nameof(Id), nameof(CategoryId))]
    public class InventoryModel
    {
        public int Id { get; set; }

        public CategoryId CategoryId { get; set; }

        public ProductModel Product { get; set; } = null!;

        public int Quantity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = new byte[0];

        public DateTime CreatedAt { get; set; }
    }
}
