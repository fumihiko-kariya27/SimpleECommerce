using Microsoft.AspNetCore.Mvc;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Service.Catalog;

namespace SimpleECommerce.Controllers.Catalog
{
    internal class ProductController : Controller
    {
        private readonly IProductService service;

        internal ProductController(IProductService service) 
        { 
            this.service = service;
        }
    }
}
