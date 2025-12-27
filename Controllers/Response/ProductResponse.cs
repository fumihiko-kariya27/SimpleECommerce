using SimpleECommerce.Domain.Catalog;
using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Controllers.Response
{
    public class ProductResponse
    {
        [Display(Name = "商品ID")]
        public string Id { get; }

        [Display(Name = "商品名")]
        public string Name { get; }

        [Display(Name = "説明")]
        public string? Description { get; }

        [DataType(DataType.Currency)]
        [Display(Name = "価格")]
        public int Price { get; }

        public ProductResponse(Product domainProduct)
        {
            this.Id = domainProduct.Id.Code;
            this.Name = domainProduct.Name.Name;
            this.Description = domainProduct.Description.Desc;
            this.Price = domainProduct.Price.price;
        }
    }
}
