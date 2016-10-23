using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Ninject;
using Ninject.Web.Common;
using Service.Entities;
using Service.Repositories;
using Service.Tests.App_Start;

namespace Service.Tests
{
    [TestClass()]
    public class StockExchangeServiceTests
    {
        public StockExchangeServiceTests()
        {
            
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // arrange
            
            IKernel kernel = new StandardKernel();
            var stockRepoMock = new Mock<IStockRepository>();
            stockRepoMock.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            kernel.Bind<IStockRepository>().ToConstant(stockRepoMock.Object);
            kernel.Bind<StockExchangeService>().ToSelf();

            NinjectWebCommon.Kernel = kernel;
            NinjectWebCommon.Start();


            var service = kernel.Get<StockExchangeService>();
            service.Authentication = new Authentication() {User = "testuser", Password = "testpassword"};
            //act
            var response = service.GetAll();
            //assert
            Assert.AreEqual(response.Count, 1000);
            Assert.AreEqual(response.Page, 1);
            Assert.AreEqual(response.Stocks.Count, 15);
            Assert.AreEqual(response.Stocks.First().Id, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void Getall_return_403_error()
        {
            // arrange

            IKernel kernel = new StandardKernel();
            var stockRepoMock = new Mock<IStockRepository>();
            stockRepoMock.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            kernel.Bind<IStockRepository>().ToConstant(stockRepoMock.Object);
            kernel.Bind<StockExchangeService>().ToSelf();

            NinjectWebCommon.Kernel = kernel;
            NinjectWebCommon.Start();


            var service = kernel.Get<StockExchangeService>();
            service.Authentication = new Authentication() { User = "badguy", Password = "hehe" };
            //act
            var response = service.GetAll();
            //assert
        }


        [TestMethod()]
        public void GetByIdsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }
    }
}