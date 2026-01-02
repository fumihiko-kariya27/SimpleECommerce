using Microsoft.AspNetCore.Mvc;
using SimpleECommerce.Controllers.Request;
using SimpleECommerce.Controllers.Response;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Exception;
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

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Category,Id,Name,Desc,Price")] ProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                Product product = request.ToDomain();
                await service.RegisterAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (ProductAlreadyExistException ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
        }

        public async Task<IActionResult> IsUniqueProduct([Bind("Category,Id")] ProductRequest request)
        {
            Product product = request.ToDomain();
            if (await service.IsUniqueProduct(product))
            {
                return Json("指定の商品コードは既に登録されています");
            }
            return Json(true);
        }

        public async Task<ProductCsvResponse> Csv()
        {
            IReadOnlyList<Product> ret = await service.ListAsync();
            IEnumerable<ProductResponse> response = ret.Select(p => new ProductResponse(p)).ToList();
            return new ProductCsvResponse(response);
        }
    }
}
