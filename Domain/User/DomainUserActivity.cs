namespace SimpleECommerce.Domain.User
{
    public class DomainUserActivity
    {
        public DateTime LastLogin { get; }

        public DomainUserActivity(DateTime lastLogin) 
        {
            ArgumentNullException.ThrowIfNull(lastLogin);

            LastLogin = lastLogin;
        }
    }
}
