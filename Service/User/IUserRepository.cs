using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.User
{
    public interface IUserRepository
    {
        IDomainUser? SelectByIdAsync(CustomerId id);
    }
}
