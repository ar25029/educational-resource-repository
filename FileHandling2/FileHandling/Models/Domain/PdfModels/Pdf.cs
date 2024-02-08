using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FileHandling.Models.Domain.Pdf
{
    public class Pdf
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string? ResourceName { get; set; }

        public string? ResourcePdf { get; set; }

        [NotMapped]
        public IFormFile PdfFile { get; set; }
    }
}
