using FileHandling.Models.Domain.ImageModels;
using FileHandling.Repository.Abstract.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FileHandling.Repository.Implementation.Images
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ImageContext _context;

        public ProductImageRepository(ImageContext context)
        {
            _context = context;
        }
        public bool AddImage(Image model)
        {
            try
            {
                _context.Images.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteImage(string name)
        {

            List<Image> list = _context.Images.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name)
                    {
                        _context.Images.Remove(product);
                        _context.SaveChanges();
                        return true;

                    }


                }

            }
            return false;
        }


        public string GetImageName(string name)
        {
            List<Image> list = _context.Images.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name)
                    {

                        return product.ResourceImage;

                    }


                }

            }
            return "Check the imagename";

        }


    }
}
