using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Client.Models;
using Client.ServiceAgents;
using Common;
using X.PagedList;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStockService _stockService;

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

        public ActionResult Refresh(int page, int size)
        {
            var stocks = AsyncHelper.RunSync(() => _stockService.GetAllAsync(page, size));
            var listItems = stocks.Stocks.Select(s => new StockListItem() { Id = s.Id, Price = s.Price });
            var vm = new StaticPagedList<StockListItem>(listItems, page, size, stocks.Count);
            return PartialView(vm);
        }
    }
}