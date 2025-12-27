namespace SimpleECommerce.Domain.Catalog
{
    internal class ProductName
    {
        public string Name { get; init; } = string.Empty;

        internal ProductName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) 
            {
                throw new ArgumentException("商品名に空白文字のみ、あるいはnullは設定できません");
            }

            this.Name = name.Trim();
        }
    }
}
