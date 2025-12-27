using SimpleECommerce.Domain.Catalog;

namespace SimpleECommerce.Controllers.Response
{
    internal class ProductResponse
    {
        private readonly string _id;

        private readonly string _name;

        private readonly string? _description;

        private readonly int _price;

        internal ProductResponse(Product domainProduct)
        {
            this._id = domainProduct.Id.Code;
            this._name = domainProduct.Name.Name;
            this._description = domainProduct.Description.Desc;
            this._price = domainProduct.Price.price;
        }
    }
}
