using System.Data.Entity;
using System.Threading.Tasks;
using Client.Entities;

namespace Client.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserByIdIncludeStocksAsync(string userId)
        {
            return await _context.Users.Include(s => s.Stocks).FirstOrDefaultAsync(s => s.Id == userId);
        }
    }
}