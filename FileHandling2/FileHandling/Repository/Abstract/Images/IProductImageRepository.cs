using FileHandling.Models.Domain.ImageModels;

namespace FileHandling.Repository.Abstract.Images
{
    public interface IProductImageRepository
    {
        bool AddImage(Image model);
       
        bool DeleteImage(string name);

        public string GetImageName(string name);
    }
}
