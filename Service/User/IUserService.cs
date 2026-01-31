using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.User
{
    public interface IUserService
    {
        IDomainUser? FindAsync(CustomerId id);
    }
}
