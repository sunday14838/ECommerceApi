using AutoMapper;
using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using ECommerceApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace ECommerceApi.Services
{
#pragma warning disable CS1591
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        private readonly ILogger<ProductService> logger;
        private readonly IMemoryCache cache;

        public ProductService(IProductRepository productRepository, IMapper mapper, IImageService imageService,
            ILogger<ProductService> logger,
            IMemoryCache cache)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.imageService = imageService;
            this.logger = logger;
            this.cache = cache;
        }

        public async Task<List<Product>> GetAll()
        {
            List<Product> products = (await productRepository.GetAllAsync()).ToList();
            return products;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(ProductQueryParameters parameters)
        {
            //var products = await productRepository.GetProductsAsync(parameters);

            //return mapper.Map<IEnumerable<ProductResponseDto>>(products);

            const string cacheKey = "products";

            if (!cache.TryGetValue(cacheKey, out IEnumerable<ProductResponseDto>? products))
            {
                logger.LogInformation("Loading products from database");

                var data = await productRepository.GetProductsAsync(parameters);

                products = mapper.Map<IEnumerable<ProductResponseDto>>(data);

                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                cache.Set(cacheKey, products, options);
            }
            else
            {
                logger.LogInformation("Products loaded from cache");
            }

            return products ?? Enumerable.Empty<ProductResponseDto>();
        }

        public async Task<ProductResponseDto> GetById(int id) 
        {
            string cacheKey = $"product_{id}";

            //var product =   await productRepository.GetById(id);
            //if (product == null)
            //{
            //    throw new Exception("Product not found");
            //}

            if (cache.TryGetValue(cacheKey, out ProductResponseDto? product) && product != null)
            {
                return product;
            }

            var entity = await productRepository.GetById(id);

                if (entity == null)
                    throw new Exception("Product not found.");

                product = mapper.Map<ProductResponseDto>(entity);

                cache.Set(cacheKey, product, TimeSpan.FromMinutes(10));
            

            return product;
        }

        public async Task<ApiResponse> Create(CreateProductDto dto)
        {
            var product = mapper.Map<Product>(dto);

            product.ImageUrl = await imageService.UploadImageAsync(dto.Image);

             await productRepository.Add(product);
            logger.LogInformation("Creating product {ProductName}", product.Name);

            await productRepository.SaveChangesAsync();
            logger.LogInformation("Product {ProductName} created successfully", product.Name);

            cache.Remove("products");

            return new ApiResponse
            {
                Success = true,
                Message = "Product created successfully"
            };
        }

        public async Task<ApiResponse> Update(int id, UpdateProductDto dto)
        {
            var product =   await productRepository.GetById(id);

            if (product == null)
            throw new Exception("Product not found");
            logger.LogWarning("Product with ID {ProductId} was not found", id);

            mapper.Map(dto, product);

            if (dto.Image != null)
            {
                imageService.DeleteImage(product.ImageUrl);

                product.ImageUrl =
                    await imageService.UploadImageAsync(dto.Image);
            }

            productRepository.Update(product);

            await productRepository.SaveChangesAsync();

            cache.Remove("products");

            return new ApiResponse
            {
                Success = true,
                Message = "Product updated successfully"
            };
        }

        public async Task<ApiResponse> Delete(int id)
        {
            var product = await productRepository.GetById(id); ;
            if (product == null) throw new Exception("Product not found");

            logger.LogWarning("Product with ID {ProductId} was not found", id);

            imageService.DeleteImage(product.ImageUrl);

            productRepository.Delete(product);

            await productRepository.SaveChangesAsync();

            cache.Remove("products");

            return new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            };
        }
    }
}
