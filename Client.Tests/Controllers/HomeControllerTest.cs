using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Client.Controllers;
using Client.Entities;
using Client.localhost;
using Client.Models;
using Client.Repositories;
using Client.ServiceAgents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using X.PagedList;

namespace Client.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTest
    {
        [TestMethod()]
        public async Task Buy_modelstate_is_invalid_Test()
        {
            // arrange
            var controller = new HomeController(null, null, null);
            controller.ModelState.AddModelError("test", "test");

            // act
            var result = await controller.Buy(new BuyStock() { Id = 12, Count = 12, Price = 122 }) as ViewResult;
            var model = result.Model as BuyStock;
            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);

            Assert.AreEqual(model.Id, 12);
            Assert.AreEqual(model.Count, 12);
            Assert.AreEqual(model.Price, 122);

        }

        [TestMethod()]
        public async Task Buy_post_valid_model_Test()
        {
            // arrange
            var stocks = new List<Stock>();
            for (int i = 0; i < 20; i++)
            {
                stocks.Add(new Stock() {Id = i+1, Count = 10, RemoteId = i+1});
            }


            var stockServiceMock = new Mock<IStockService>();
            stockServiceMock.Setup(s => s.GetByRemoteId(It.IsAny<int>()))
                .Returns<int>(s => new GetByIdResponseMessage() {Stock = new StockDetails() {Id = s, Price = 123} });

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(s => s.GetUserByIdIncludeStocksAsync(It.IsAny<string>()))
                .Returns<string>(s => Task.FromResult(new ApplicationUser() {Id = s, Stocks = stocks }));

            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(s => s.SaveChangesAsync()).Returns(Task.FromResult(1));

            Mock<ControllerContext> controllerContextMock = new Mock<ControllerContext>();

            var claim = new ClaimsIdentity(new List<Claim>() {new Claim(ClaimTypes.NameIdentifier, "userid")}) {};
            controllerContextMock.Setup(s => s.HttpContext.User).Returns(new ClaimsPrincipal(claim));


            var controller = new HomeController(stockServiceMock.Object, contextMock.Object, userRepoMock.Object)
            {
                ControllerContext = controllerContextMock.Object
            };

            // act
            var result = await controller.Buy(new BuyStock() { Id = 12, Count = 12, Price = 122 }) as RedirectToRouteResult;
            //var model = result.Model as BuyStock;
            //assert
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public async Task Sell_modelstate_is_invalid_Test()
        {
            // arrange
            var controller = new HomeController(null, null, null);
            controller.ModelState.AddModelError("test", "test");

            // act
            var result = await controller.Sell(new SellStock() { Id = 12, Count = 12, Price = 122 }) as ViewResult;
            var model = result.Model as SellStock;
            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);

            Assert.AreEqual(model.Id, 12);
            Assert.AreEqual(model.Count, 12);
            Assert.AreEqual(model.Price, 122);

        }


        [TestMethod()]
        public async Task sell_post_valid_model_Test()
        {
            // arrange
            var stocks = new List<Stock>();
            for (int i = 0; i < 20; i++)
            {
                stocks.Add(new Stock() { Id = i + 1, Count = 10, RemoteId = i + 1 });
            }


            var stockServiceMock = new Mock<IStockService>();
            stockServiceMock.Setup(s => s.GetByRemoteId(It.IsAny<int>()))
                .Returns<int>(s => new GetByIdResponseMessage() { Stock = new StockDetails() { Id = s, Price = 123 } });

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(s => s.GetUserByIdIncludeStocksAsync(It.IsAny<string>()))
                .Returns<string>(s => Task.FromResult(new ApplicationUser() { Id = s, Stocks = stocks }));

            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(s => s.SaveChangesAsync()).Returns(Task.FromResult(1));

            Mock<ControllerContext> controllerContextMock = new Mock<ControllerContext>();

            var claim = new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, "userid") }) { };
            controllerContextMock.Setup(s => s.HttpContext.User).Returns(new ClaimsPrincipal(claim));


            var controller = new HomeController(stockServiceMock.Object, contextMock.Object, userRepoMock.Object)
            {
                ControllerContext = controllerContextMock.Object
            };

            // act
            var result = await controller.Sell(new SellStock() { Id = 12, Count = 5, Price = 122 }) as RedirectToRouteResult;
            //var model = result.Model as BuyStock;
            //assert
            Assert.IsNotNull(result);
        }
    }
}

namespace Client.Tests.Controllers
{
    [TestClass()]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var stocks = new List<StockDetails>();
            for (int i = 0; i < 20; i++)
            {
                stocks.Add(new StockDetails() { Id = i + 1, Price = (new Random()).Next(1, 1000) });
            }
            var mock = new Mock<IStockService>();
            mock.Setup(s => s.GetAll(1, 20)).Returns<int, int>((page, size) => new GetAllResponseMessage()
            {
                Count = 1000,
                Stocks = stocks.ToArray(),
                Page = 1,
                Size = 20
            });


            HomeController controller = new HomeController(mock.Object, null, null);

            // Act
            ViewResult result = controller.Index(1, 20) as ViewResult;
            var model = result.Model as StaticPagedList<StockListItem>;
            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(model.Count, 20);
            Assert.AreEqual(model.TotalItemCount, 1000);
            Assert.AreEqual(model.PageNumber, 1);
            Assert.AreEqual(model.PageSize, 20);
        }


        [TestMethod()]
        public void BuyTest()
        {
            // Arrange
            var mock = new Mock<IStockService>();
            mock.Setup(s => s.GetByRemoteId(It.IsAny<int>()))
                .Returns<int>(s =>
                {
                    if (s > 0 && s<=1000)
                    {
                        return new GetByIdResponseMessage() { Stock = new StockDetails() { Id = s } };
                    }
                    return new GetByIdResponseMessage() {Stock = null};
                });


            var controller = new HomeController(mock.Object, null, null);

            // act
            var result = controller.Buy(123) as ViewResult;
            var model = result.Model as BuyStock;

            var result2 = controller.Buy(111111) as HttpNotFoundResult;

            // assert
            Assert.IsNotNull(result2);

            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(model.Id, 123);
        }
    }
}
