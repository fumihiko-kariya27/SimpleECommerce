using Microsoft.AspNetCore.Mvc;

namespace SimpleECommerce.Controllers.Purchase
{
    public class PurchaseController : Controller
    {
        public async Task<IActionResult> purchase(PurchaseRequest request)
        {
            return View();
        }
    }
}
