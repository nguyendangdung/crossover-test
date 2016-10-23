using System.Data.Entity;
using System.Threading.Tasks;

namespace Service.Entities
{
    public interface IContext
    {
        DbSet<Stock> Stocks { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}