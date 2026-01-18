using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleECommerce.Domain.Catalog;
using SimpleECommerce.Domain.Catalog.Categories;

namespace SimpleECommerce.Models.User.Authorization
{
    [PrimaryKey(nameof(RoleId), nameof(PermissionId))]
    public class RolePermissionModel
    {
        public int RoleId { get; set; }

        public RoleModel Role { get; set; } = null!;

        public int PermissionId { get; set; }

        public PermissionModel Permission { get; set; } = null!;
    }
}
