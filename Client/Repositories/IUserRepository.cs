using System;
using System.Collections.Generic;
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
}
