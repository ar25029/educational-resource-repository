using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FileHandling.Models.Domain.ImageModels
{
    public class Image
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string? ResourceName { get; set; }

        public string? ResourceImage { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
