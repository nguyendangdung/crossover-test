using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace Client
{
    public class NinjectDI : IDependencyResolver
    {
        public IKernel Kernel;

        public NinjectDI()
        {
            Kernel = new StandardKernel();
        }
        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}