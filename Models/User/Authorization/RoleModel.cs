using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Models.User.Authorization
{
    public class RoleModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<UserRoleModel> Users { get; } = new List<UserRoleModel>();

        public ICollection<RolePermissionModel> Permissions { get; } = new List<RolePermissionModel>();
    }
}
