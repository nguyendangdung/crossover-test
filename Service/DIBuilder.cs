using System;
using Autofac;
using Service.Entities;
using Service.Repositories;

namespace Service
{
    public static class DIBuilder
    {
        public static void Configure(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterType<Context>().As<IContext>().InstancePerRequest();
            builder.RegisterType<StockRepository>().As<IStockRepository>();
        }
    }
}
