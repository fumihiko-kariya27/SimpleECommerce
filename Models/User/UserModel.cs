using SimpleECommerce.Models.Purchase;

namespace SimpleECommerce.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ICollection<UserRoleModel> Roles { get; } = [];

        public ICollection<PurchasePointHistoryModel> PurchaseHistories { get; } = [];

        public DateTime? LastLogin { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
