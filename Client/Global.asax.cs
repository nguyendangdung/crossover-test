using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Client.Entities;
using Client.Repositories;
using Client.ServiceAgents;
using Ninject.Web.Common;

namespace Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static NinjectDI DI = new NinjectDI();
        protected void Application_Start()
        {
            DI.Kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();
            DI.Kernel.Bind<IUserRepository>().To<UserRepository>();
            DI.Kernel.Bind<IStockService>().To<StockService>();
            DependencyResolver.SetResolver(DI);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
