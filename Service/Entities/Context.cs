using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Service.Entities
{
    public class Context : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }


        public List<Stock> GetRandomStocks()
        {
            var stocks = new List<Stock>();
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                stocks.Add(new Stock() {Price = random.Next(0, 1000)});
            }
            return stocks;
        }
    }
}