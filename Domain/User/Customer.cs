using SimpleECommerce.Domain.Purchase.Choise;

namespace SimpleECommerce.Domain.User
{
    internal class Customer : IDomainUser
    {
        private readonly string _name;
        private readonly CustomerId _Id;
        private readonly ShoppingCart _cart = new ();

        internal Customer(string name, string id)
        { 
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(id);

            this._name = name;
            this._Id = new CustomerId(id);
        }

        public string Id => _Id.Value;
    }
}
