namespace ECommerceApi.Services.Interfaces
{
#pragma warning disable CS1591
    public interface IImageService
    {
        Task<string?> UploadImageAsync(IFormFile? image);
        void DeleteImage(string? imagePath);
    }
}
