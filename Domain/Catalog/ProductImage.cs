namespace SimpleECommerce.Domain.Catalog
{
    public class ProductImage
    {
        // 画像ファイルが存在しない場合は以下の画像を使用する
        private static readonly Uri defalutImage = new Uri("/images/noimage.jpg", UriKind.Relative);

        public Uri Path { get; init; }

        public ProductImage(Uri path) 
        {
            if (!path.ToString().EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)) 
            {
                // 商品画像はjpg形式のみを許可する
                throw new ArgumentException("商品画像の拡張子はjpg形式のみが許可されています");
            }

            Path = path;
        }

        internal static ProductImage NoImage()
        {
            return new ProductImage(defalutImage);
        }
    }
}
