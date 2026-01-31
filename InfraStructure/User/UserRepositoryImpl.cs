using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User;
using SimpleECommerce.Service.User;

namespace SimpleECommerce.InfraStructure.User
{
    internal class UserRepositoryImpl : IUserRepository
    {
        private readonly ECommerceDbContext _context;

        public UserRepositoryImpl(ECommerceDbContext context)
        { 
            _context = context;
        }

        public IDomainUser? SelectByIdAsync(CustomerId id)
        {
            UserModel? user = _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Email.Equals(id.Value)).FirstOrDefault();
            if (user == null) 
            {
                return null;
            }

            string? role = user.Roles.FirstOrDefault()?.RoleId.ToString();
            DomainUserRole userRole = DomainUserRole.Unknown;
            if (role != null && Enum.IsDefined(typeof(DomainUserRole), Int32.Parse(role)))
            {
                userRole = (DomainUserRole)Int32.Parse(role);
            }

            string name = user.Name;
            string email = user.Email;

            return DomainUserFactory.CreateUserByRole(userRole, name, email);
        }
    }
}
