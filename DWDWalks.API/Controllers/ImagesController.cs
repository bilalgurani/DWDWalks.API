using DWDWalks.API.Models.Domain;
using DWDWalks.API.Models.DTO;
using DWDWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DWDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> upload([FromForm] ImageUploadRequestDto imageUpload)
        {
            ValidateFileUpload(imageUpload);

            if (ModelState.IsValid)
            {
                // Convert from DTO to domain model
                var imageModel = new Image
                {
                    File = imageUpload.File,
                    FileExtention = Path.GetExtension(imageUpload.File.FileName),
                    FileSizeInBytes = imageUpload.File.Length,
                    FileName = imageUpload.FileName,
                    FileDescription = imageUpload.FileDescription
                };

                // User repository to upload image
                await imageRepository.Upload(imageModel);

                return Ok(imageModel);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequest)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequest.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (imageUploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload smaller file");
            }
        }
    }
}
