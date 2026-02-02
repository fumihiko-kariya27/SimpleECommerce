namespace SimpleECommerce.Domain.Auth
{
    public class Role
    {
        public int Id { get; }

        public IEnumerable<Permission> Permissions { get; } = [];

        public Role(int id, IEnumerable<Permission> permissions)
        {
            ArgumentNullException.ThrowIfNull(permissions);

            Id = id;
            Permissions = permissions;
        }
    }
}
