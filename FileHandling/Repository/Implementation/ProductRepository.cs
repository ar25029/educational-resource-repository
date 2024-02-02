using FileHandling.Models.Domain;
using FileHandling.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FileHandling.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }
        public bool Add(Product model)
        {
            try
            {
                _context.Products.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Delete(string name)
        {

            List<Product> list = _context.Products.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ProductName == name)
                    {
                        _context.Products.Remove(product);
                        _context.SaveChanges();
                        return true;

                    }
                    

                }

            }
            return false;
        }


        public string GetName(string name)
        {
            List<Product> list = _context.Products.ToList();
            //Product temp = new Product();
            string ?tempName = "";
            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ProductName == name)
                    {
                        //int ?id = product.Id;
                        //Product temp = _context.Products.Find(id);
                        //if (temp != null)
                        //{
                        //     tempName = temp.ProductImage;
                        //    return tempName;
                        //}
                        return product.ProductImage;
                       
                       
                    }
                   

                }

            }
         return "Check the imagename";
        
        }


    }
}
