using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.Repositories
{
    public interface IStockRepository
    {
        IQueryable<Stock> GetAll();
        IQueryable<Stock> GetByIds(List<int> ids);
        Stock GetById(int id);
    }
}
