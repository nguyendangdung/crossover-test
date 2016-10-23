using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace Service
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ContainerBuilder Builder = new ContainerBuilder();
        public static IContainer Container = ConfigureContainer();
        private static IContainer ConfigureContainer()
        {
            DIBuilder.Configure(Builder);
            Builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = Builder.Build();
            return container;
        }
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
