using DWDWalks.API.Models.Domain;

namespace DWDWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
