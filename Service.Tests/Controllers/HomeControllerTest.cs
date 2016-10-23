using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
