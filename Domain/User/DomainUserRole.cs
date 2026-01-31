namespace SimpleECommerce.Domain.User
{
    public enum DomainUserRole
    {
        // 未設定
        Unknown = 0,

        // 管理者
        Admin = 1,

        // 運用担当者
        Operator = 2,

        // 利用者
        Customer = 3
    }
}
