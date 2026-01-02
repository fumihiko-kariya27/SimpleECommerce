using Microsoft.AspNetCore.Mvc;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SimpleECommerce.Controllers.Request
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "{0}は必須です")]
        [Display(Name = "カテゴリ")]
        public CategoryId Category { get; set; }

        [Remote("IsUniqueProduct", "Product", AdditionalFields = nameof(Category))]
        [Required(ErrorMessage = "{0}は必須です")]
        [Range(0, 9999, ErrorMessage = "{0}は{1}～{2}の間で指定してください")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}は必須です")]
        [StringLength(20, ErrorMessage = "{0}は{1}文字以内で指定してください")]
        [Display(Name = "商品名")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "{0}は{1}文字以内で指定してください")]
        [Display(Name = "説明")]
        public string Desc { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0}は必須です")]
        [Range(0, 100000, ErrorMessage = "{0}は{1}～{2}の間で指定してください")]
        [Display(Name = "価格")]
        public int Price { get; set; }

        internal Product ToDomain()
        {
            ProductName name = new ProductName(Name);
            Description description = new Description(Desc);
            ProductPrice price = new ProductPrice(Price);
            return new Product(Category, Id, name, description, price);
        }
    }
}
