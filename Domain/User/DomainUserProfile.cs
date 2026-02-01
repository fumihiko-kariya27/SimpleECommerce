using System.Text.RegularExpressions;

namespace SimpleECommerce.Domain.User
{
    public class DomainUserProfile
    {
        public string Name { get; }

        public string Email { get; }

        private static Regex mailAddressFormat = new Regex(@"^[a-zA-Z0-9._]+@[a-zA-Z0-9.]+.[a-zA-Z]{2,}$");

        public DomainUserProfile(string name, string email)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

            if (!mailAddressFormat.IsMatch(email))
            {
                throw new InvalidDataException($"メールアドレスが正しい形式で入力されていません {email}");
            }

            Name = name;
            Email = email;
        }
    }
}
