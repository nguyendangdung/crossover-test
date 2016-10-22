using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Service.Entities;
using Service.Models;
using Service.Repositories;

namespace Service.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;
        private IStockRepository _stockRepository;

        public HomeController(Context context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        public ActionResult Index(int page = 1, int size = 15)
        {
            var stocks = _stockRepository.GetAll().OrderBy(s => s.Id).Pager(page, size).ToList();
            var vm = stocks.Select(s => new StockListitem()
            {
                Id = s.Id,
                Price = s.Price
            });
            return View(vm);
        }
    }
}