using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.RegularExpressions;

namespace SimpleECommerce.Domain.User
{
    public class CustomerId : IEquatable<CustomerId>
    {
        private static readonly int Min = 1;

        private readonly int _id;

        public int Value => _id;

        internal CustomerId(int id)
        {
            if (id < Min)
            {
                throw new ArgumentOutOfRangeException($"Idは{Min}以上の値でなければいけません");
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

            return this._id == other._id;
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
