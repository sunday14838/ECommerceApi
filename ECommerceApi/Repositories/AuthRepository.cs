using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Repositories
{
#pragma warning disable CS1591
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {

        public AuthRepository(AppDbContext context) : base(context)
        {
        }

        public  async Task<bool> Exists(string email)
        {
           return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
