using FluentValidation;
using StudentCourseRegistrationAPI.Data;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationAPI.Validator;
using Scalar.AspNetCore;

namespace StudentCourseRegistrationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register FluentValidation validators from this assembly so IValidator<T> can be injected
            builder.Services.AddValidatorsFromAssemblyContaining<StudentRegistrationValidator>();

            // Configure OpenAPI/Swagger
            builder.Services.AddOpenApi();

            // Configure EF Core
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference("/");
            }

            app.UseAuthorization();

            app.UseRouting();
            
            app.MapControllers();

            app.Run();
        }
    }
}
