using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Client.Models;
using Client.ServiceAgents;
using X.PagedList;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private IStockService _stockService;

        public HomeController(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<ActionResult> Index(int page = 1, int size = 15)
        {
            var stocks = await _stockService.GetAllAsync(page, size);
            var listItems = stocks.Stocks.Select(s => new StockListItem() {Id = s.Id, Price = s.Price});
            var vm = new StaticPagedList<StockListItem>(listItems, page, size, stocks.Count);
            return View(vm);
        }
    }
}