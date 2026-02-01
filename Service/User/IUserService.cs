using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.User
{
    public interface IUserService
    {
        Task<DomainUser?> FindByEmailAsync(string email);

        Task<DomainUser?> FindByIdAsync(CustomerId id);

        Task<string> GetHashedPasswordAsync(string email);
    }
}
