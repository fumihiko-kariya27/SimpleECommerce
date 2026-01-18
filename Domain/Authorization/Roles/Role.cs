using SimpleECommerce.Domain.Authorization.Permission;

namespace SimpleECommerce.Domain.Authorization.Roles
{
    public interface Role
    {
        bool HasPermission(Permissions permissions);
    }
}
