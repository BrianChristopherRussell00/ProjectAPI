using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using ProjectAPI.Models.Domain;
using ProjectAPI.Models.DTOs;
using ProjectAPI.Repository;

namespace ProjectAPI.Controllers
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
        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {       
                //Convert DTO to Domain Model   
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,  
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName), 
                    FileSizeInBytes = imageUploadRequestDto.File.Length,    
                    FileName = imageUploadRequestDto.FileName, 
                    FileDescription = imageUploadRequestDto.FileDescription
                    
                };


                // User repository to upload image
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }   
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }       
            // If more than 10 megabytes in bytes
            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file","File size is more than 10MB, Please upload a smaller file");
            }
        }

    }
}
