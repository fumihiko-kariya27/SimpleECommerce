using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;
using SimpleECommerce.Service.Catalog;
using SimpleECommerce.Service.Image;

namespace SimpleECommerce.Controllers.Catalog
{
    public class ProductController : Controller
    {
        private readonly IProductService service;

        public ProductController(IProductService service) 
        { 
            this.service = service;
        }

        [Authorize(Policy = "ViewProduct")]
        public async Task<IActionResult> Index()
        {
            IReadOnlyList<Product> ret = await service.ListAsync();
            IEnumerable<ProductResponse> response = ret.Select(p => new ProductResponse(p)).ToList();
            return View(response);
        }

        [Authorize(Policy = "ViewProduct")]
        [HttpGet("Product/Edit/{category}/{productId}/{sequence}")]
        public async Task<IActionResult> Images(int categoryId, int productId, int sequence)
        {
            if (!Enum.IsDefined(typeof(CategoryId), categoryId)) 
            {
                return NotFound();
            }

            ProductId id = new ProductId((CategoryId)categoryId, productId);
            ProductImage image = await service.GetImageAsync(id, sequence);
            return File(image.Data, image.ContentType);
        }

        public IActionResult Create()
        { 
            return View(new ProductRequest());
        }

        [Authorize(Policy = "RegisterNewProduct")]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Category,Id,Name,Desc,Price,UploadFiles")] ProductRequest request)
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
            catch (ImageSizeOutOfRangeException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
            catch (ProductAlreadyExistException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
        }

        [HttpGet("Product/Edit/{category}/{productId}")]
        public async Task<IActionResult> Edit(int category, int productId)
        {
            if (!Enum.IsDefined(typeof(CategoryId), category))
            {
                return NotFound();
            }

            try
            {
                ProductId id = new ProductId((CategoryId)category, productId);
                Product product = await service.Get(id);
                return View(ProductRequest.GetEditOrigin(product));
            }
            catch (ProductNotExistException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return NotFound();
            }
        }

        [Authorize(Policy = "UpdateProduct")]
        [HttpPost("Product/Edit")]
        public async Task<IActionResult> Edit([Bind("Category,Id,Name,Desc,Price,UploadFiles")] ProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            Product product = request.ToDomain();

            if (!await service.IsExistAsync(product))
            {
                return NotFound();
            }

            await service.ModifyAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "DeleteProduct")]
        [HttpGet("Product/Delete/{category}/{productId}")]
        public async Task<IActionResult> Delete(int category, int productId)
        {
            if (!Enum.IsDefined(typeof(CategoryId), category))
            {
                return NotFound();
            }

            await service.Delete(new ProductId((CategoryId)category, productId));
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "ViewProduct")]
        public async Task<IActionResult> IsUniqueProduct([Bind("Category,Id")] ProductRequest request)
        {
            Product product = request.ToDomain();
            if (await service.IsUniqueProduct(product))
            {
                return Json("指定の商品コードは既に登録されています");
            }
            return Json(true);
        }

        [Authorize(Policy = "ViewProduct")]
        public async Task<ProductCsvResponse> Csv()
        {
            IReadOnlyList<Product> ret = await service.ListAsync();
            IEnumerable<ProductResponse> response = ret.Select(p => new ProductResponse(p)).ToList();
            return new ProductCsvResponse(response);
        }
    }
}
