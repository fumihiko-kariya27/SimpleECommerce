using Microsoft.IdentityModel.Tokens;
using SimpleECommerce.Domain.Exception;
using System.Text.RegularExpressions;

namespace SimpleECommerce.Domain.Catalog
{
    public class ProductImage
    {
        private const int maxDataSize = 1024 * 1024;

        public string FileName { get; init; } = string.Empty;
        public string ContentType { get; init; } = string.Empty;
        public byte[] Data { get; init; } = new byte[0];

        public ProductImage(string fileName, string contentType, byte[] data)
        {
            if (String.IsNullOrWhiteSpace(fileName)) 
            {
                throw new ArgumentException("ファイル名に空白文字は設定できません");
            }

            if (String.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentException("コンテンツタイプに空白文字は設定できません");
            }

            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("画像データが設定されていません");
            }

            if (data.Length > maxDataSize) 
            {
                throw new ImageSizeOutOfRangeException($"画像サイズは{maxDataSize}byteまでで指定してください");
            }

            FileName = fileName;
            ContentType = contentType;
            Data = data;
        }
    }
}
