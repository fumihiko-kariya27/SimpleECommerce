using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.User;
using System.Linq.Expressions;

namespace SimpleECommerce.Service.User
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<DomainUser>> SelectAsync(Expression<Func<UserModel, bool>> predicate);

        Task<string> SelectHashedPasswordAsync(string email);
    }
}
