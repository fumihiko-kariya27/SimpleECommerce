
namespace SimpleECommerce.Domain.User
{
    public interface IDomainUser
    {
        public DomainUserRole Type { get; }

        public string Id { get; }

        public string Name { get; }
    }
}
