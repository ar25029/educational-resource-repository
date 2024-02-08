using FileHandling.Models.Domain;
using FileHandling.Models.Domain.Pdf;

namespace FileHandling.Repository.Abstract.Pdfs
{
    public interface IPdfService
    {
        public Tuple<int, string> SavePdf(IFormFile imageFile);

        public bool DeletePdf(string pdfFileName);
        public Tuple<int, byte[], string> GetPdf(string fileName);
        
    }
}
