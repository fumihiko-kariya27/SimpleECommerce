using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SimpleECommerce.Domain.Catalog
{
    internal class Description
    {
        private readonly string? description;

        internal Description(string? description) 
        {
            // 説明のないアイテムもある可能性を踏まえ、nullを許可する
            this.description = description?.Trim();
        }

        internal string Desc
        {
            get
            {
                return this.description ?? string.Empty;
            }
        }
    }
}
