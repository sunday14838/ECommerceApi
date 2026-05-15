using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Services
{
    public class ProductService
    {
        private readonly AppDbContext context;

        public ProductService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Product> GetAll()
        {
            var products = context.Products.ToList();
            return products;
        }

        public Product GetById(int id) 
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return product;
        }

        public string Create(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            context.Products.Add(product);
            context.SaveChanges();

            return "Product created";
        }

        public string Update(int id, UpdateProductDto dto)
        {
            var product = context.Products.FirstOrDefault(x=> x.Id == id);
            if (product == null) throw new Exception("Product not found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;

            context.SaveChanges();

            return "Product updated";
        }

        public string Delete(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id); ;
            if (product == null) throw new Exception("Product not found");

            context.Products.Remove(product);
            context.SaveChanges();

            return "Product deleted";
        }
    }
}
