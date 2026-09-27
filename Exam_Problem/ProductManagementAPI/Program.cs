using ProductManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ProductManagementAPI.Validators;
using ProductManagementAPI.DTOs;
using Scalar.AspNetCore;

namespace ProductManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register FluentValidation validators
            builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            // Register OpenAPI / Swagger
            builder.Services.AddOpenApi();

            // Register EF Core DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference("/");
            }

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
