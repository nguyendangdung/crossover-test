using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;
using Service.Repositories;

namespace Service.Tests
{    
    class MockContext : IContext
    {
        public IDbSet<Stock> Stocks { get; set; }


        public MockContext()
        {
            Stocks = (IDbSet<Stock>)GetRandomStocks();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }


        public Collection<Stock> GetRandomStocks()
        {
            var stocks = new Collection<Stock>();
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                stocks.Add(new Stock() { Price = random.Next(0, 1000) });
            }
            return stocks;
        }
    }
}
