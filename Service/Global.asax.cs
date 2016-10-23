using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Web.Common;
using Service.Entities;
using Service.Repositories;

namespace Service
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static NinjectDI DI = new NinjectDI();
        protected void Application_Start()
        {

            DI.Kernel.Bind<IContext>().To<Context>().InRequestScope();
            DI.Kernel.Bind<IStockRepository>().To<StockRepository>();
            DependencyResolver.SetResolver(DI);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
