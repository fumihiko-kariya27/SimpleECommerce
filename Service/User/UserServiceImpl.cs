using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.User
{
    internal class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _repository;

        public UserServiceImpl(IUserRepository repository)
        { 
            _repository = repository;
        }

        public IDomainUser? FindAsync(CustomerId id)
        {
            return _repository.SelectByIdAsync(id);
        }
    }
}
