using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FileHandling.Models.Domain.Pdf
{
    public class Pdf
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string? ResourceName { get; set; }

        [DefaultValue("PDF")]
        public string? ResourceCategory { get; set; }

        public string Description { get; set; }

        public DateTime ? CreatedDate { get; set; } 

        [Required]
        public int? Standard {  get; set; }

        public string? ResourcePdf { get; set; }

        [NotMapped]
        public IFormFile PdfFile { get; set; }
    }
}
