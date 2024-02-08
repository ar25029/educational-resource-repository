using Microsoft.EntityFrameworkCore;

namespace FileHandling.Models.Domain.VideoModels

{
    public class VideoContext : DbContext
    {
        public VideoContext(DbContextOptions<VideoContext> options) : base(options)
        {

        }

        public DbSet<Video> Videos { get; set; }
    }
}
