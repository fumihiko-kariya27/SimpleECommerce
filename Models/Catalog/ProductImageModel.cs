using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Models.Catalog
{
    [PrimaryKey(nameof(CategoryId), nameof(ProductId), nameof(Sequence))]
    public class ProductImageModel
    {
        public CategoryId CategoryId { get; set; }

        public CategoryModel Category { get; set; } = null!;

        public int ProductId { get; set; }

        public ProductModel Product { get; set; } = null!;

        public int Sequence { get; set; }

        public byte[] ImageData { get; set; } = Array.Empty<byte>();

        public string ContentType { get; set; } = "image/jpeg";

        public string FileName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
