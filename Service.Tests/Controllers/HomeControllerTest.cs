using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using Service.Controllers;
using Service.Entities;
using Service.Models;
using Service.Repositories;

namespace Service.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_return_not_null_ViewResult()
        {
            // Arrange

            var mockStockRepo = new Mock<IStockRepository>();
            mockStockRepo.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            var controller = new HomeController(null, mockStockRepo.Object);

            //// Act
            ViewResult result = controller.Index() as ViewResult;

            //// Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index_return_result_contain_15_elements()
        {
            // Arrange
            var mockStockRepo = new Mock<IStockRepository>();
            mockStockRepo.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());

            var controller = new HomeController(null, mockStockRepo.Object);

            //// Act
            ViewResult result = controller.Index() as ViewResult;
            var model = result.Model as IEnumerable<StockListitem>;
            //// Assert
            Assert.AreEqual(model.Count(), 15);
        }

        [TestMethod]
        public void Index_return_result_items_in_page_2()
        {
            // Arrange
            var mockStockRepo = new Mock<IStockRepository>();
            mockStockRepo.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            var controller = new HomeController(null, mockStockRepo.Object);

            //// Act
            ViewResult result = controller.Index(2) as ViewResult;
            var model = result.Model as IEnumerable<StockListitem>;
            //// Assert
            Assert.AreEqual(model.Count(), 15);
            Assert.AreEqual(model.First().Id, 16);
        }

        [TestMethod]
        public void Edit_get_return_not_found_result()
        {
            // Arrange
            var mockStockRepo = new Mock<IStockRepository>();
            mockStockRepo.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            var controller = new HomeController(null, mockStockRepo.Object);

            //// Act
            var result = controller.Edit(100002);

            // Assert
            Assert.IsTrue(result is HttpNotFoundResult);
        }


        [TestMethod]
        public void Edit_get_return_stock_item()
        {
            // Arrange
            var mockStockRepo = new Mock<IStockRepository>();
            mockStockRepo.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            mockStockRepo.Setup(s => s.GetById(It.IsInRange(1, 1000, Range.Inclusive)))
                .Returns<int>(s => new Stock() {Id = s});
            var controller = new HomeController(null, mockStockRepo.Object);

            //// Act
            var result = controller.Edit(102) as ViewResult;

            // Assert
            var model = result.Model as StockDetails;
            Assert.IsTrue(model.Id == 102);
        }


        [TestMethod]
        public void Edit_post_ModalState_not_valid()
        {
            // arrange
            var mockStockRepo = new Mock<IStockRepository>();
            mockStockRepo.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            mockStockRepo.Setup(s => s.GetById(It.IsInRange(1, 1000, Range.Inclusive)))
                .Returns<int>(s => new Stock() { Id = s });

            var controller = new HomeController(null, mockStockRepo.Object);
            controller.ModelState.AddModelError("test", "test");
            var model = new StockDetails() {Id = 1, Price = 33333};

            // act
            var result = controller.Edit(model) as ViewResult;
            var rModel = result.Model as StockDetails;
            // assert
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1);
            Assert.IsTrue(rModel != null);
            Assert.AreEqual(rModel.Id, 1);
            Assert.AreEqual(rModel.Price, 33333);
        }

        [TestMethod]
        public void Edit_post_ModalState_is_valid()
        {
            // arrange
            var mockStockRepo = new Mock<IStockRepository>();
            mockStockRepo.Setup(s => s.GetAll()).Returns(() => (new Context()).GetRandomStocks().AsQueryable());
            mockStockRepo.Setup(s => s.GetById(It.IsInRange(1, 1000, Range.Inclusive)))
                .Returns<int>(s => new Stock() { Id = s });


            var contextMock = new Mock<IContext>();
            contextMock.Setup(s => s.SaveChanges()).Returns(1);

            var controller = new HomeController(contextMock.Object, mockStockRepo.Object);
            var model = new StockDetails() { Id = 1, Price = 33333 };

            // act
            var result = controller.Edit(model);
            // assert
            Assert.IsTrue(result is RedirectToRouteResult);
        }
    }
}
