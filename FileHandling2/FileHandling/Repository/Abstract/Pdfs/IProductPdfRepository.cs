
using FileHandling.Models.Domain.Pdf;

namespace FileHandling.Repository.Abstract.Pdfs
{
    public interface IProductPdfRepository
    {
        bool AddPdf(Pdf model);

        bool DeletePdf(string name);

        public string GetPdfName(string name);

        public Pdf GetPdf(string name);

        public List<Pdf> GetAllPdfs();
        public List<Pdf> GetPdfByStandard(int standard);
    }
}
