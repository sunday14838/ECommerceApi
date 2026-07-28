using ECommerceApi.Models;

namespace ECommerceApi.Repositories.Interfaces
{
#pragma warning disable CS1591
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetUserOrders(int userId);
    }

}
