using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Entities;

namespace Client.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdIncludeStocksAsync(string userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserByIdIncludeStocksAsync(string userId)
        {
            return await _context.Users.Include(s => s.Stocks).FirstOrDefaultAsync(s => s.Id == userId);
        }
    }
}
