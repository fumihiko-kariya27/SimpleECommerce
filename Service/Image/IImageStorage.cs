namespace SimpleECommerce.Service.Image
{
    public interface IImageStorage
    {
        Task<Uri> SaveAsync(IFormFile file, string path);
    }
}
