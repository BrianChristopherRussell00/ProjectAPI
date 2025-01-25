using ProjectAPI.Data;
using ProjectAPI.Models.Domain;

namespace ProjectAPI.Repository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly BrianRussellDbContext brianRussellDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, BrianRussellDbContext brianRussellDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.brianRussellDbContext = brianRussellDbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
               $"{image.FileName}{image.FileExtension}");
                    
            //Upload Image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);  
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host} {httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            
            image.FilePath = urlFilePath;   

            // Add Image to the Image table 
            await brianRussellDbContext.Images.AddAsync(image);
            await brianRussellDbContext.SaveChangesAsync();

            return image;
        }   

    }
}
