using ECommerceApi.Models;

namespace ECommerceApi.Repositories.Interfaces
{
#pragma warning disable CS1591
    public interface ICartRepository : IGenericRepository<CartItem>
    {
        Task<CartItem?> GetItem(int userId, int productId);

        Task<List<CartItem>> GetUserCart(int userId);
        void DeleteRange(IEnumerable<CartItem> cartItems);
    }
}
