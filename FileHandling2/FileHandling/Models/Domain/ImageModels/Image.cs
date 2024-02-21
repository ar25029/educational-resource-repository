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

        public int? Standard { get; set; }

        public string? Category {  get; set; }

        [Required]
        public string? ResourceDescription { get; set; }


        [Required]
        public DateTime? DateCreated { get; set; }

        public string? ResourceImage { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
