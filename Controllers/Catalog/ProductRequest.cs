using Microsoft.AspNetCore.Mvc;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Domain.Catalog.Factory;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SimpleECommerce.Controllers.Catalog
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
        public string? Desc { get; set; }

        [Required(ErrorMessage = "{0}は必須です")]
        [Range(0, 100000, ErrorMessage = "{0}は{1}～{2}の間で指定してください")]
        [Display(Name = "価格")]
        public int Price { get; set; }

        [Display(Name = "商品画像")]
        public IFormFile[] UploadFiles { get; set; } = Array.Empty<FormFile>();

        internal Product ToDomain(ProductFactory factory)
        {
            Product ret = factory.Create(Id, Category, Name, Desc ?? "", Price);
            foreach (IFormFile file in UploadFiles)
            {
                byte[] data = Array.Empty<byte>();
                using (MemoryStream st = new MemoryStream())
                {
                    file.CopyTo(st);
                    data = st.ToArray();
                }

                ret.Images.Add(new ProductImage(file.FileName, file.ContentType, data));
            }

            return ret;
        }

        internal static ProductRequest GetEditOrigin(Product org)
        {
            ProductRequest ret = new();
            ret.Category = org.Id.Category;
            ret.Id = org.Id.Value;
            ret.Name = org.Name.Value;
            ret.Desc = org.Description.Value;
            ret.Price = org.Price.Value;
            return ret;
        }
    }
}
