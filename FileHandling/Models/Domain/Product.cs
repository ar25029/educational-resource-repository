using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHandling.Models.Domain
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string? ProductName { get; set; }

        public string? ProductImage { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
