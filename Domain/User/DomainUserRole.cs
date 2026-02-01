using SimpleECommerce.Domain.Auth;

namespace SimpleECommerce.Domain.User
{
    public class DomainUserRole
    {
        public IEnumerable<Role> Roles { get; } = [];

        public DomainUserRole(IEnumerable<Role> roles) 
        {
            Roles = roles;
        }
    }
}
