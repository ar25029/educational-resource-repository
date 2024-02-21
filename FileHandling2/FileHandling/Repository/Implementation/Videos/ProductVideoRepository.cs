using FileHandling.Models.Domain.Pdf;
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
                        product.Flag = 0;
                        //_context.Pdfs.Remove(product);
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
            return "Video not found";

        }



        public List<Video> GetAllVideos()
        {
            return _context.Videos.ToList();
        }

        public List<Video> GetVideoByStandard(int standard)
        {
            List<Video> list = _context.Videos.ToList();
            List<Video> temp = new List<Video>();

            foreach (var product in list)
            {
                if (product.Standard == standard && product.Flag == 1)
                {
                    temp.Add(product);
                }
            }
            return temp;
        }

        public Video GetVideo(string name)
        {
            List<Video> list = _context.Videos.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name)
                    {

                        return product;

                    }
                }
            }
            return null;
        }

        public List<Video> GetAllPublishableVideos(int std)
        {
            List<Video> list = _context.Videos.ToList();
            List<Video> all = new List<Video>();
            foreach (var product in list)
            {
                if (product.Standard == std && product.Flag == 2)
                {
                    all.Add(product);
                }
            }
            return all;
        }

        public int PublishVideo(string name, int std)
        {
            List<Video> list = _context.Videos.ToList();
            DateTime date = DateTime.Now;
            foreach (var item in list)
            {
                if (item.ResourceName == name && item.Standard == std)
                {
                    item.Flag = 1;
                    item.DateCreated = date;
                    _context.SaveChanges();
                    return 1;
                }
            }
            return 0;
        }
    }
}
