using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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

        public async Task<DomainUser?> FindByEmailAsync(string email)
        {
            IReadOnlyCollection<DomainUser> users = await _repository.SelectAsync(user => user.Email.Equals(email));
            // メールアドレスが合致するユーザーは1名しかいないはずなので先頭データを返却する
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task<DomainUser?> FindByIdAsync(CustomerId id)
        {
            IReadOnlyCollection<DomainUser> users = await _repository.SelectAsync(user => user.Id == id.Value);
            // IDが合致するユーザーは１名しかいないはずなので先頭データを返却する
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task<string> GetHashedPasswordAsync(string email)
        {
            return await _repository.SelectHashedPasswordAsync(email);
        }
    }
}
