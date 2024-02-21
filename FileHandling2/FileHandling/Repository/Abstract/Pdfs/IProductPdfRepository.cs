
using FileHandling.Models.Domain.Pdf;

namespace FileHandling.Repository.Abstract.Pdfs
{
    public interface IProductPdfRepository
    {
        bool AddPdf(Pdf model);

        bool DeletePdf(string name, DateTime date);

        public string GetPdfName(string name, DateTime date);

        public Pdf GetPdf(string name);

        public List<Pdf> GetAllPdfs();

        public List<Pdf> GetAllPublishablePdfs(int std);
        public List<Pdf> GetPdfByStandard(int standard);


        public int PublishPdf(string name, int std);
    }
}
