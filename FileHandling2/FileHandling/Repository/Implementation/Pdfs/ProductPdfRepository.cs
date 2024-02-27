using FileHandling.data;
using FileHandling.Models.Domain.Pdf;
using FileHandling.Repository.Abstract.Pdfs;
using Microsoft.EntityFrameworkCore;

namespace FileHandling.Repository.Implementation.Pdfs
{
    public class ProductPdfRepository : IProductPdfRepository
    {
        private readonly FileContext _context;

        public ProductPdfRepository(FileContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPdf(Pdf model)
        {
            try
            {
                _context.Pdfs.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeletePdf(string name, int id)
        {

            List<Pdf> list = await _context.Pdfs.ToListAsync();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name && product.Id == id)
                    {
                        product.Flag = 0;
                        //_context.Pdfs.Remove(product);
                        await _context.SaveChangesAsync();
                        return true;

                    }


                }

            }
            return false;
        }


        public async Task<string> GetPdfName(string name, DateTime date)
        {
            List<Pdf> list = await _context.Pdfs.ToListAsync();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name && product.DateCreated == date)
                    {

                        return product.ResourcePdf;

                    }


                }

            }
            return "Check the Pdf name";

        }


        public async Task<Pdf> GetPdf(string name)
        {
            List<Pdf> list = await _context.Pdfs.ToListAsync();

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


        public async Task<List<Pdf>> GetAllPdfs()
        {
            return await _context.Pdfs.ToListAsync();
        }

        public async Task<List<Pdf>> GetAllPublishablePdfs(int std)
        {
            List<Pdf> list = await _context.Pdfs.ToListAsync();
            List<Pdf> all = new List<Pdf>();
            foreach (var product in list)
            {
                if (product.Standard == std && product.Flag == 2)
                {
                    all.Add(product);
                }
            }
            return all;
        }

        public async Task<List<Pdf>> GetPdfByStandard(int std)
        {
            List<Pdf> list = await _context.Pdfs.ToListAsync();
            List<Pdf> temp = new List<Pdf>();

            foreach (var product in list)
            {
                if (product.Standard == std && product.Flag == 1)
                {
                    temp.Add(product);
                }
            }
            return temp;
        }



        public async Task<List<Pdf>> GetAllDeletedPdfs(int std)
        {
            List<Pdf> list = await _context.Pdfs.ToListAsync();
            List<Pdf> all = new List<Pdf>();
            foreach (var product in list)
            {
                if (product.Standard == std && product.Flag == 0)
                {
                    all.Add(product);
                }
            }
            return all;
        }


        public async Task<int> PublishPdf(string name, int std)
        {
            List<Pdf> list = await _context.Pdfs.ToListAsync();
            DateTime date = DateTime.Now;
            foreach (var item in list)
            {
                if (item.ResourceName == name && item.Standard == std)
                {
                    item.Flag = 1;
                    item.DateCreated = date;
                    await _context.SaveChangesAsync();
                    return 1;
                }
            }
            return 0;
        }
    }
}
