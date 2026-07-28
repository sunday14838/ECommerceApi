using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApi.Repositories
{
#pragma warning disable CS1591
    public class CartRepository : GenericRepository<CartItem>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public void DeleteRange(IEnumerable<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems);
        }

        public async Task<CartItem?> GetItem(int userId, int productId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }

        public async Task<List<CartItem>> GetUserCart(int userId)
        {
            return await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();
        }
    }
}
