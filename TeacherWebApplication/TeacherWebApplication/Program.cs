
using Microsoft.EntityFrameworkCore;
using TeacherWebApplication.Data;
using TeacherWebApplication.Models.EntityModels;
using TeacherWebApplication.Services;

namespace TeacherWebApplication
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

            builder.Services.AddDbContext<TeacherDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("teacherDb"));
            });

            builder.Services.AddTransient<ITeacherService, TeacherService>();

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
