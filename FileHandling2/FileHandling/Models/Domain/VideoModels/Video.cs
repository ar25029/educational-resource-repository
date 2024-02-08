using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FileHandling.Models.Domain.VideoModels
{
    public class Video
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string? ResourceName { get; set; }

        public string? ResourceVideo { get; set; }

        [NotMapped]
        public IFormFile VideoFile { get; set; }
    }
}
