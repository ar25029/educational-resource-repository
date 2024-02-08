using FileHandling.Models.Domain.VideoModels;

namespace FileHandling.Repository.Abstract.Videos
{
    public interface IProductVideoRepository
    {
        bool AddVideo(Video model);

        bool DeleteVideo(string name);

        public string GetVideoName(string name);
    }
}
