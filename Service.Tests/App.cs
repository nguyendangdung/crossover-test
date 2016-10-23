using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Web.Common;

namespace Service.Tests
{
    class App : NinjectHttpApplication
    {

        protected override IKernel CreateKernel()
        {
            throw new NotImplementedException();
        }
    }
}
