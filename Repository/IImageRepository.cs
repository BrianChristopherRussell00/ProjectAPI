using ProjectAPI.Models.Domain;

namespace ProjectAPI.Repository
{
    public interface IImageRepository
    {   
       Task<Image> Upload(Image image);

    }
}
