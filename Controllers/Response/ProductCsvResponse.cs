using Microsoft.AspNetCore.Mvc;
using SimpleECommerce.Domain.Catalog;
using System.Reflection;
using System.Text;

namespace SimpleECommerce.Controllers.Response
{
    public class ProductCsvResponse : ActionResult
    {
        private readonly IEnumerable<ProductResponse> products;

        internal ProductCsvResponse(IEnumerable<ProductResponse> products)
        { 
            this.products = products;
        }

        public override void ExecuteResult(ActionContext context)
        {
            HttpResponse response = context.HttpContext.Response;
            response.Headers.ContentType = "text/csv; charset=sjis";
            response.Headers.ContentDisposition = $"attachment; filename={DateTime.Now.ToString("yyyyMMdd-HHmmss")}_productList.csv";
            response.WriteAsync(ToCsv(products), Encoding.GetEncoding("Shift-JIS"));
        }

        private string ToCsv(IEnumerable<ProductResponse> products)
        { 
            StringBuilder sb = new StringBuilder();
            foreach (ProductResponse product in products)
            { 
                List<string?> row = new List<string?>();
                foreach (PropertyInfo prop in product.GetType().GetProperties())
                {
                    Type propType = prop.PropertyType;
                    if (propType.IsPrimitive || propType == typeof(string) || propType == typeof(DateTime))
                    {
                        // プリミティブ型、文字列、日時情報の場合のみ出力対象とし、ナビゲーションプロパティ等は含めない
                        row.Add(prop.GetValue(product)?.ToString());
                    }
                }
                sb.AppendLine(string.Join(",", row.ToArray()));
            }
            return sb.ToString();
        }
    }
}
