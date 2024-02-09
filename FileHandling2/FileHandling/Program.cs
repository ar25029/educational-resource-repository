using FileHandling.Models.Domain.Pdf;
using FileHandling.Models.Domain.VideoModels;
using FileHandling.Repository.Abstract;
using FileHandling.Repository.Abstract.Images;
using FileHandling.Repository.Abstract.Pdfs;
using FileHandling.Repository.Abstract.Videos;
using FileHandling.Repository.Implementation.Images;
using FileHandling.Repository.Implementation.Pdfs;
using FileHandling.Repository.Implementation.Videos;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace FileHandling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
           

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ImageContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });


            builder.Services.AddDbContext<VideoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });


            builder.Services.AddDbContext<PdfContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });


            builder.Services.AddTransient<IImageService, ImageService>();
            builder.Services.AddTransient<IProductImageRepository, ProductImageRepository>();


            builder.Services.AddTransient<IPdfService, PdfService>();
            builder.Services.AddTransient<IProductPdfRepository, ProductPdfRepository>();

            builder.Services.AddTransient<IVideoService, VideoService>();
            builder.Services.AddTransient<IProductVideoRepository, ProductVideoRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}