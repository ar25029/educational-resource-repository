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
                        product.Flag = 0;
                        //_context.Pdfs.Remove(product);
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
                    if (product.ResourceName == name )
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

        public List<Pdf> GetAllPublishablePdfs(int std)
        {
            List<Pdf> list = _context.Pdfs.ToList();
            List<Pdf> all = new List<Pdf>();
            foreach (var product in list)
            {
                if(product.Standard == std && product.Flag == 2)
                {
                    all.Add(product);
                }
            }
            return all;
        }

        public List<Pdf> GetPdfByStandard(int std)
        {
            List<Pdf> list = _context.Pdfs.ToList();
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

        public int PublishPdf(string name, int std)
        {
            List<Pdf> list = _context.Pdfs.ToList();
            DateTime date = DateTime.Now;
            foreach (var item in list)
            {
                if(item.ResourceName == name && item.Standard == std)
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
