using Application.Common.Interfaces;

namespace ECommerce.API.Services
{
    public class FileService : IFileService
    {
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IWebHostEnvironment _env { get; }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var formFile = file as FormFile;
            if (formFile == null)
                throw new ArgumentException("Invalid File");


            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
            var folderPath = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(folderPath);

            var savePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(savePath, FileMode.Create);
            await formFile.CopyToAsync(stream);

            return $"/images/{fileName}";
        }
    }
}
