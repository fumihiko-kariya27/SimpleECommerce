using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Models.Catalog;

[PrimaryKey(nameof(Id), nameof(CategoryId))]
public class ProductModel
{
    public int Id { get; set; }

    public CategoryId CategoryId { get; set; } = CategoryId.None;

    public CategoryModel Category { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Desc { get; set; } = string.Empty;

    public int Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    internal Product ToDomain()
    {
        ProductName productName = new (Name);
        Description description = new(Desc);
        ProductPrice price = new(Price);
        return new(CategoryId, Id, productName, description, price);
    }
}
