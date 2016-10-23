using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Service.Controllers;
using Service.Entities;
using Service.Repositories;

namespace Service.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IContext _context;
        private IStockRepository _stockRepository;
        private HomeController _controller;
        public HomeControllerTest()
        {
            _context = new MockContext();
            _stockRepository = new StockRepository(_context);
            _controller = new HomeController(_context, _stockRepository);
        }

        [TestMethod]
        public void Index_return_not_null_ViewResult()
        {
            //// Act
            ViewResult result = _controller.Index() as ViewResult;

            //// Assert
            Assert.IsNotNull(result);
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
