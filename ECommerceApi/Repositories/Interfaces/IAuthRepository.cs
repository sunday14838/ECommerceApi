using ECommerceApi.Models;

namespace ECommerceApi.Repositories.Interfaces
{
#pragma warning disable CS1591
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<bool> Exists(string email);
        Task<User?> GetByEmail(string email);
    }
}
