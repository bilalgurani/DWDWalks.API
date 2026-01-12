
using DWDWalks.API.Data;

namespace DWDWalks.API.Repositories
{
    public class LocalImageRepository: IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DWDWalksDBContext dBContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, DWDWalksDBContext dBContext) 
        { 
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dBContext = dBContext;
        }

        public async Task<Models.Domain.Image> Upload(Models.Domain.Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");

            // Upload image to local storage
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;

            // Add image to the image database table
            await dBContext.Images.AddAsync(image);
            await dBContext.SaveChangesAsync();

            return image;
        }
    }
}
