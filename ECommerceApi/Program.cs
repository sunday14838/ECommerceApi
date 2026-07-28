using ECommerceApi.Extensions;
using ECommerceApi.Mappings;
using ECommerceApi.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ECommerceApi
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container using extension methods
            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(MappingProfile).Assembly); });

            builder.Services.AddHealthChecks();

            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddSwaggerAndVersioningServices();
            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
