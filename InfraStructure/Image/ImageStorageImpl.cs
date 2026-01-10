using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NuGet.Protocol.Plugins;
using SimpleECommerce.Service.Image;

namespace SimpleECommerce.InfraStructure.Image
{
    public class ImageStorageImpl : IImageStorage
    {
        // 画像ファイルを保存するルートディレクトリ
        private readonly string _imageRoot;
        // 画像ファイルを保存するサブディレクトリ
        private readonly string _subDir = "images";

        public ImageStorageImpl(IWebHostEnvironment env)
        {
            _imageRoot = Path.Combine(env.WebRootPath, _subDir);
        }

        public async Task<Uri> SaveAsync(IFormFile file, string relativePath)
        {
            string fullPath = Path.Combine(_imageRoot, relativePath);
            string? dir = Path.GetDirectoryName(fullPath);

            if (dir != null)
            { 
                Directory.CreateDirectory(dir);
            }

            using Stream stream = new FileStream(fullPath, FileMode.CreateNew);
            await file.CopyToAsync(stream);

            return new Uri($"{_subDir}/{relativePath}", UriKind.Relative);
        }
    }
}
