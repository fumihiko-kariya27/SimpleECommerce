using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Models.Catalog;
using SimpleECommerce.Models.User;

namespace SimpleECommerce.Models.Shopping
{
    public class CartLineModel
    {
        public string Id { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public CategoryId Category { get; set; } = CategoryId.None;

        public string ProductName { get; set; } = string.Empty;

        public int ProductPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ProductModel Product { get; set; } = null!;

        public UserModel User { get; set; } = null!;
    }
}
