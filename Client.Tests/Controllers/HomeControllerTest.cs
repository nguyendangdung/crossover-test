using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using Client.Controllers;
using Client.localhost;
using Client.Models;
using Client.ServiceAgents;
using Moq;
using X.PagedList;

namespace Client.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var stocks = new List<StockDetails>();
            for (int i = 0; i < 20; i++)
            {
                stocks.Add(new StockDetails() {Id = i+1, Price = (new Random()).Next(1, 1000)});
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

        [TestMethod]
        public void About()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.About() as ViewResult;

            //// Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            //// Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Contact() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }
    }
}
