using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SimpleECommerce.Controllers.Catalog
{
    public class ProductResponse
    {
        [Display(Name = "カテゴリ")]
        public CategoryId Category { get; set; }

        [Display(Name = "商品ID")]
        public int Id { get; set; }

        [Display(Name = "商品名")]
        public string Name { get; set; }

        [Display(Name = "説明")]
        public string? Desc { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "価格")]
        public int Price { get; set; }

        public IList<int> imageSequence = new List<int>();

        public ProductResponse(Product domainProduct)
        {
            this.Category = domainProduct.Id.Category;
            this.Id = domainProduct.Id.Id;
            this.Name = domainProduct.Name.Name;
            this.Desc = domainProduct.Description.Desc;
            this.Price = domainProduct.Price.price;
            for (int i = 0; i < domainProduct.Images.Count; i++)
            {
                imageSequence.Add(i + 1);
            }
        }
    }
}
