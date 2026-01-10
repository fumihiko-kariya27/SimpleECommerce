using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SimpleECommerce.Controllers.Response
{
    public class ProductResponse
    {
        [Display(Name = "カテゴリ")]
        public int Category { get; }

        [Display(Name = "商品ID")]
        public string Id { get; }

        [Display(Name = "商品名")]
        public string Name { get; }

        [Display(Name = "説明")]
        public string? Description { get; }

        [DataType(DataType.Currency)]
        [Display(Name = "価格")]
        public int Price { get; }

        [Display(Name = "商品画像")]
        public Uri Image;

        public ProductResponse(Product domainProduct)
        {
            this.Category = (int)domainProduct.Id.Category;
            this.Id = domainProduct.Id.Id.ToString("D4");
            this.Name = domainProduct.Name.Name;
            this.Description = domainProduct.Description.Desc;
            this.Price = domainProduct.Price.price;
            this.Image = domainProduct.Image.Path;
        }
    }
}
