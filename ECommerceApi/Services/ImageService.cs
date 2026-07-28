using ECommerceApi.Services.Interfaces;

namespace ECommerceApi.Services
{
#pragma warning disable CS1591
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            var filePath = Path.Combine(
                _environment.WebRootPath,
                imagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<string?> UploadImageAsync(IFormFile? image)
        {
            if (image == null || image.Length==0) 
            {
                return null;
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName =
            $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            await image.CopyToAsync(stream);

            return $"/images/products/{fileName}";
        }
    }
}
