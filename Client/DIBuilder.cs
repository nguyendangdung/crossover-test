using System;
using Autofac;
using Client.Entities;
using Client.Repositories;
using Client.ServiceAgents;

namespace Client
{
    public static class DIBuilder
    {
        public static void Configure(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<StockService>().As<IStockService>();
        }
    }
}
