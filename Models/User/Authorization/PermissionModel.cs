using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Models.User.Authorization
{
    public class PermissionModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<RolePermissionModel> Roles { get; } = new List<RolePermissionModel>();
    }
}
