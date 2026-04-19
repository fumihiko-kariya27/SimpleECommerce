using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Controllers.Purchase
{
    public class PurchaseRequest
    {
        public CategoryId Category { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
