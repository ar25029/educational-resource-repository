using Microsoft.EntityFrameworkCore;

namespace FileHandling.Models.Domain.Pdf
{
    public class PdfContext : DbContext
    {
        public PdfContext(DbContextOptions<PdfContext> options) : base(options)
        {

        }

        public DbSet<Pdf> Pdfs { get; set; }
    }
}
