using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Domain.Catalog.Factory;
using SimpleECommerce.Models.Stock;
using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Models.Catalog;

[PrimaryKey(nameof(Id), nameof(CategoryId))]
public class ProductModel
{
    public int Id { get; set; }

    public CategoryId CategoryId { get; set; } = CategoryId.None;

    public CategoryModel Category { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Price { get; set; }

    public ICollection<ProductImageModel> Images { get; } = new List<ProductImageModel>();

    public InventoryModel Inventory { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    internal Product ToDomain(ProductFactory factory)
    {
        return factory.Create(Id, CategoryId, Name, Description, Price, Inventory.Quantity);
    }
}
