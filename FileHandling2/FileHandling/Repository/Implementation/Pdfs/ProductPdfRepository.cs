using FileHandling.Models.Domain.Pdf;
using FileHandling.Repository.Abstract.Pdfs;
using Microsoft.EntityFrameworkCore;

namespace FileHandling.Repository.Implementation.Pdfs
{
    public class ProductPdfRepository : IProductPdfRepository
    {
        private readonly PdfContext _context;

        public ProductPdfRepository(PdfContext context)
        {
            _context = context;
        }

        public bool AddPdf(Pdf model)
        {
            try
            {
                _context.Pdfs.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeletePdf(string name)
        {

            List<Pdf> list = _context.Pdfs.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name)
                    {
                        _context.Pdfs.Remove(product);
                        _context.SaveChanges();
                        return true;

                    }


                }

            }
            return false;
        }


        public string GetPdfName(string name)
        {
            List<Pdf> list = _context.Pdfs.ToList();

            if (list != null)
            {
                foreach (var product in list)
                {
                    if (product.ResourceName == name)
                    {

                        return product.ResourcePdf;

                    }


                }

            }
            return "Check the Pdf name";

        }


        public Pdf GetPdf(string name)
        {
            List<Pdf> list = _context.Pdfs.ToList();

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


        public List<Pdf> GetAllPdfs()
        {
            return _context.Pdfs.ToList();
        }


        public List<Pdf> GetPdfByStandard(int std)
        {
            List<Pdf> list = _context.Pdfs.ToList();
            List<Pdf> temp = new List<Pdf>();

            foreach (var product in list)
            {
                if (product.Standard == std)
                {
                    temp.Add(product);
                }
            }
            return temp;
        }
    }
}
