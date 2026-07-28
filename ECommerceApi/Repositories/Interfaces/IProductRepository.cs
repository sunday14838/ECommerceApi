using ECommerceApi.DTOs;
using ECommerceApi.Models;

#pragma warning disable CS1591
namespace ECommerceApi.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product> 
    {
        Task<Product?> GetById(int id);
        Task<IEnumerable<Product>> GetProductsAsync(ProductQueryParameters parameters);
    }
}
