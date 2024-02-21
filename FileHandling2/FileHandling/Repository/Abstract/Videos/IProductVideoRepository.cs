using FileHandling.Models.Domain.Pdf;
using FileHandling.Models.Domain.VideoModels;

namespace FileHandling.Repository.Abstract.Videos
{
    public interface IProductVideoRepository
    {
        bool AddVideo(Video model);

        bool DeleteVideo(string name);

        public string GetVideoName(string name);

        public Video GetVideo(string name);

        public List<Video> GetAllVideos();

        public List<Video> GetAllPublishableVideos(int std);
        public List<Video> GetVideoByStandard(int standard);

        public int PublishVideo(string name, int std);
    }
}
