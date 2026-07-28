using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApi.Repositories
{
#pragma warning disable CS1591
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ProductQueryParameters parameters)
        {
            IQueryable<Product> query = _context.Products;

            // Search
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(p =>
                    p.Name.Contains(parameters.Search));
            }

            // Minimum price
            if (parameters.MinPrice.HasValue)
            {
                query = query.Where(p =>
                    p.Price >= parameters.MinPrice.Value);
            }

            // Maximum price
            if (parameters.MaxPrice.HasValue)
            {
                query = query.Where(p =>
                    p.Price <= parameters.MaxPrice.Value);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                switch (parameters.SortBy.ToLower())
                {
                    case "price":
                        query = parameters.SortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(p => p.Price)
                            : query.OrderBy(p => p.Price);
                        break;

                    case "name":
                        query = parameters.SortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(p => p.Name)
                            : query.OrderBy(p => p.Name);
                        break;

                    default:
                        query = query.OrderBy(p => p.Id);
                        break;
                }
            }

            query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize);

            return await query.ToListAsync();
        }
    }
}
