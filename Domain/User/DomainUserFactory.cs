using System.Security.Claims;

namespace SimpleECommerce.Domain.User
{
    internal class DomainUserFactory
    {
        internal static Customer CreateCustomer(string name, string email)
        {
            return new Customer(name, email);
        }
    }
}
