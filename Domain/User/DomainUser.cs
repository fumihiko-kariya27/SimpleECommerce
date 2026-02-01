namespace SimpleECommerce.Domain.User
{
    public class DomainUser
    {
        public int Id { get; }

        public DomainUserProfile Profile { get; }

        public DomainUserActivity Activity { get; }

        public DomainUserRole Role { get; }

        public DomainUser(int id, DomainUserProfile profile, DomainUserActivity activity, DomainUserRole role)
        {
            ArgumentNullException.ThrowIfNull(profile);
            ArgumentNullException.ThrowIfNull(activity);
            ArgumentNullException.ThrowIfNull(role);

            Id = id;
            Profile = profile;
            Activity = activity;
            Role = role;
        }
    }
}
