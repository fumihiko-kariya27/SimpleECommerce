using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Models.User.Authorization;

namespace SimpleECommerce.Models.User
{
    [PrimaryKey(nameof(UserId), nameof(RoleId))]
    public class UserRoleModel
    {
        public int UserId { get; set; }

        public UserModel User { get; set; } = null!;

        public int RoleId { get; set; }

        public RoleModel Role { get; set; } = null!;
    }
}
