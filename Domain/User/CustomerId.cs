using System.Text.RegularExpressions;

namespace SimpleECommerce.Domain.User
{
    public class CustomerId : IEquatable<CustomerId>
    {
        // IDはメールアドレスを使用するものとする
        private static Regex rules = new Regex(@"^[a-zA-Z0-9._]+@[a-zA-Z0-9.]+.[a-zA-Z]{2,}$");

        private readonly string _id;

        public string Value => _id;

        internal CustomerId(string id)
        {
            if (!rules.IsMatch(id))
            {
                throw new CustomerIdIllegalFormatException($"IDはメールアドレス形式で設定してください [Specified ID = {id}]");
            }

            _id = id;
        }

        public bool Equals(CustomerId? other)
        {
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            return this._id.Equals(other._id);
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as CustomerId);
        }

        public override int GetHashCode()
        {
            return this._id.GetHashCode();
        }
    }
}
