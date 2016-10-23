using System.Collections.Generic;
using System.Linq;
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
