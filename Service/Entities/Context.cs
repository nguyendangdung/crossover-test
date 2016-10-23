using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using Service.Migrations;

namespace Service.Entities
{
    public class Context : DbContext, IContext
    {
        public IDbSet<Stock> Stocks { get; set; }

        public Context()
        {
            Database.SetInitializer<Context>(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        }

        public List<Stock> GetRandomStocks()
        {
            var stocks = new List<Stock>();
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                stocks.Add(new Stock()
                {
                    Price = random.Next(0, 1000),
                    Id = i + 1
                });
            }
            return stocks;
        }
    }
}