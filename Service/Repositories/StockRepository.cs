using System;
using System.Collections.Generic;
using System.Linq;
using Service.Entities;

namespace Service.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly Context _context;

        public StockRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Stock> GetAll()
        {
            return _context.Stocks;
        }

        public IQueryable<Stock> GetByIds(List<int> ids)
        {
            return GetAll().Where(s => ids.Contains(s.Id));
        }

        public Stock GetById(int id)
        {
            return _context.Stocks.Find(id);
        }
    }
}