using ECommerceApi.Data;
using ECommerceApi.Repositories;
using ECommerceApi.Repositories.Interfaces;
using ECommerceApi.Services;
using ECommerceApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS1591
namespace ECommerceApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            services.AddMemoryCache();

            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IImageService, ImageService>();

            // Services
            services.AddScoped<PasswordService>();
            services.AddScoped<JwtService>();
            services.AddScoped<AuthService>();
            services.AddScoped<ProductService>();
            services.AddScoped<CartService>();
            services.AddScoped<OrderService>();
            services.AddScoped<ImageService>();

            return services;
        }
    }
}
