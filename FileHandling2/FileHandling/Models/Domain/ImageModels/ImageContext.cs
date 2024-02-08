using FileHandling.Models.Domain.ImageModels;
using Microsoft.EntityFrameworkCore;

public class ImageContext : DbContext
{
    public ImageContext(DbContextOptions<ImageContext> options) : base(options)
    {

    }

    public DbSet<Image> Images { get; set; }


}
