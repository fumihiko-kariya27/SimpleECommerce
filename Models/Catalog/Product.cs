using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Models.Catalog;

public class Product
{
    [MaxLength(7)]
    public string Id { get; set; } = string.Empty;

    public CategoryId CategoryId { get; set; } = CategoryId.None;

    public Category Category { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
