using Azure.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Auth;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User;
using SimpleECommerce.Models.User.Authorization;
using SimpleECommerce.Service.User;
using System.Linq.Expressions;

namespace SimpleECommerce.InfraStructure.User
{
    internal class UserRepositoryImpl : IUserRepository
    {
        private readonly ECommerceDbContext _context;

        public UserRepositoryImpl(ECommerceDbContext context)
        { 
            _context = context;
        }

        public async Task<IReadOnlyList<DomainUser>> SelectAsync(Expression<Func<UserModel, bool>>? predicate = null)
        {
            IQueryable<UserModel> query = _context.Users.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query.Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(ur => ur.Permissions)
                .ThenInclude(rp => rp.Permission);

            List<DomainUser> users = [];
            foreach (var user in await query.ToListAsync()) 
            {
                IList<Role> roles = [];
                foreach (var rl in user.Roles.Select(r => r.Role))
                {
                    List<Permission> permissions = [];
                    foreach (var prm in rl.Permissions.Select(p => p.Permission))
                    {
                        Permission permission = new Permission(prm.Id, prm.Code);
                        permissions.Add(permission);
                    }
                    Role role = new Role(rl.Id, permissions);
                    roles.Add(role);
                }

                DomainUserProfile profile = new DomainUserProfile(user.Name, user.Email);
                DomainUserActivity activity = new DomainUserActivity(user.LastLogin ?? DateTime.Now);
                DomainUserRole userRole = new DomainUserRole(roles);
                users.Add(new DomainUser(user.Id, profile, activity, userRole));
            }

            return users;
        }

        public async Task<string> SelectHashedPasswordAsync(string email)
        {
            UserModel? user = await _context.Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
            return user?.Password ?? "";
        }
    }
}
