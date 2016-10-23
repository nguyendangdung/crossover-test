using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Moq;
using Ninject;
using Service.Entities;
using Service.Repositories;

namespace Service.Tests
{
    [TestClass()]
    public class StockExchangeServiceTests
    {

        [TestMethod()]
        public void GetAllTest()
        {
            // arrange

            MvcApplication.DI = new NinjectDI();



            var stockRepoMock = new Mock<IStockRepository>();
            stockRepoMock.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            MvcApplication.DI.Kernel.Bind<IStockRepository>().ToConstant(stockRepoMock.Object);
            MvcApplication.DI.Kernel.Bind<StockExchangeService>().ToSelf();


            var service = MvcApplication.DI.Kernel.Get<StockExchangeService>();
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
            MvcApplication.DI = new NinjectDI();

            var stockRepoMock = new Mock<IStockRepository>();
            stockRepoMock.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            MvcApplication.DI.Kernel.Bind<IStockRepository>().ToConstant(stockRepoMock.Object);
            MvcApplication.DI.Kernel.Bind<StockExchangeService>().ToSelf();

            var service = MvcApplication.DI.Kernel.Get<StockExchangeService>();
            service.Authentication = new Authentication() { User = "badguy", Password = "hehe" };
            //act
            var response = service.GetAll();
            //assert
        }


        [TestMethod()]
        public void GetByIdsTest()
        {
            // arrange
            MvcApplication.DI = new NinjectDI();

            var stockRepoMock = new Mock<IStockRepository>();
            stockRepoMock.Setup(s => s.GetByIds(It.IsAny<List<int>>())).Returns<List<int>>(s =>
            {
                return s.Where(t => t > 0 && t <= 1000).Select(t => new Stock() {Id = t}).AsQueryable();
            });


            MvcApplication.DI.Kernel.Bind<IStockRepository>().ToConstant(stockRepoMock.Object);
            MvcApplication.DI.Kernel.Bind<StockExchangeService>().ToSelf();


            var service = MvcApplication.DI.Kernel.Get<StockExchangeService>();
            service.Authentication = new Authentication() { User = "testuser", Password = "testpassword" };

            //act
            var response = service.GetByIds(new List<int>() {1, 2, 4, 100000});

            // assert
            Assert.AreEqual(response.Count, 3);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            // arrange
            MvcApplication.DI = new NinjectDI();

            var stockRepoMock = new Mock<IStockRepository>();
            stockRepoMock.Setup(s => s.GetById(It.IsAny<int>()))
                .Returns<int>(s =>
                {
                    if (s > 0 && s <= 1000)
                    {
                        return new Stock() { Id = s };
                    }
                    else
                    {
                        return null;
                    }
                });


            MvcApplication.DI.Kernel.Bind<IStockRepository>().ToConstant(stockRepoMock.Object);
            MvcApplication.DI.Kernel.Bind<StockExchangeService>().ToSelf();



            var service = MvcApplication.DI.Kernel.Get<StockExchangeService>();
            service.Authentication = new Authentication() { User = "testuser", Password = "testpassword" };
            //act
            var response = service.GetById(10);
            var response2 = service.GetById(100000);
            //assert
            Assert.AreEqual(response.Stock.Id, 10);
            Assert.AreEqual(response2.Stock, null);
        }
    }
}