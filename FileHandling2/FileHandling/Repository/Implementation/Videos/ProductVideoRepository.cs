using FileHandling.Models.Domain.VideoModels;
using FileHandling.Repository.Abstract.Videos;

namespace FileHandling.Repository.Implementation.Videos
{
    public class ProductVideoRepository : IProductVideoRepository
    {
        private readonly VideoContext _context;

        public ProductVideoRepository(VideoContext context)
        {
            _context = context;
        }
        public bool AddVideo(Video model)
        {
            try
            {
                _context.Videos.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteVideo(string name)
        {

            List<Video> list = _context.Videos.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name)
                    {
                        _context.Videos.Remove(product);
                        _context.SaveChanges();
                        return true;

                    }


                }

            }
            return false;
        }


        public string GetVideoName(string name)
        {
            List<Video> list = _context.Videos.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name)
                    {

                        return product.ResourceVideo;

                    }


                }

            }
            return "Check the imagename";

        }
    }
}
