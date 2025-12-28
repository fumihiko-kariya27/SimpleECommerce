using Microsoft.AspNetCore.Mvc;
using SimpleECommerce.Controllers.Response;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Service.Catalog;

namespace SimpleECommerce.Controllers.Catalog
{
    public class ProductController : Controller
    {
        private readonly IProductService service;

        public ProductController(IProductService service) 
        { 
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            IReadOnlyList<Product> ret = await service.ListAsync();
            IEnumerable<ProductResponse> response = ret.Select(p => new ProductResponse(p)).ToList();
            return View(response);
        }
    }
}
