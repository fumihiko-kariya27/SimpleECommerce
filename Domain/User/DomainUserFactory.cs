using System.Security.Claims;
using System.Security.Principal;

namespace SimpleECommerce.Domain.User
{
    internal class DomainUserFactory
    {
        internal static IDomainUser CreateUserByRole(DomainUserRole role, string name, string email)
        {
            IDomainUser? user = null;
            switch (role)
            {
                case DomainUserRole.Admin:
                    break;

                case DomainUserRole.Operator:
                    break;

                case DomainUserRole.Customer:
                    user = DomainUserFactory.CreateCustomer(name, email);
                    break;

                default:
                    throw new ArgumentException($"Unknow role [role = {role}]");
            }

            return user;
        }

        private static Customer CreateCustomer(string name, string email)
        {
            return new Customer(name, email);
        }
    }
}
