using Service.Entities;

namespace Service.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Service.Entities.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Service.Entities.Context context)
        {
            if (!context.Stocks.Any())
            {
                (context.Stocks as DbSet<Stock>).AddRange(context.GetRandomStocks());
                context.SaveChanges();
            }
        }
    }
}
