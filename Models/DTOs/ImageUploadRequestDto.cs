using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace ProjectAPI.Models.DTOs
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
            
        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }

    }
}
