using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Client.Entities;
using Client.Models;
using Client.Repositories;
using Client.ServiceAgents;
using Common;
using Microsoft.AspNet.Identity;
using X.PagedList;

namespace Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public HomeController(IStockService stockService, ApplicationDbContext context, IUserRepository userRepository)
        {
            _stockService = stockService;
            _context = context;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        public ActionResult Index(int page = 1, int size = 15)
        {
            var stocks = _stockService.GetAll(page, size);
            var listItems = stocks.Stocks.Select(s => new StockListItem() { Id = s.Id, Price = s.Price });
            var vm = new StaticPagedList<StockListItem>(listItems, page, size, stocks.Count);
            return View(vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Stock code</param>
        /// <returns></returns>

        public ActionResult Buy(int id)
        {
            var response = _stockService.GetByRemoteId(id);
            if (response.Stock == null)
            {
                return HttpNotFound();
            }

            return View(new BuyStock() { Id = response.Stock.Id, Price = response.Stock.Price, Count = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Buy(BuyStock vm)
        {
            if (ModelState.IsValid)
            {
                var response = _stockService.GetByRemoteId(vm.Id);
                if (response.Stock == null)
                {
                    return RedirectToAction("Buy", new {vm.Id});
                }
                var userId = User.Identity.GetUserId();
                var user = await _userRepository.GetUserByIdIncludeStocksAsync(userId);
                if (user.Stocks.Any(s => s.RemoteId == vm.Id))
                {
                    var stock = user.Stocks.FirstOrDefault(s => s.RemoteId == vm.Id);
                    if (stock != null)
                    {
                        stock.Count += vm.Count;
                    }
                }
                else
                {
                    user.Stocks.Add(new Stock() {RemoteId = vm.Id, Count = vm.Count});
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("MyStocks");
            }
            

            return View(vm);
        }

        public ActionResult Sell(int id)
        {
            var response = _stockService.GetByRemoteId(id);
            if (response.Stock == null)
            {
                return HttpNotFound();
            }

            return View(new SellStock() { Id = response.Stock.Id, Price = response.Stock.Price, Count = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sell(SellStock vm)
        {
            if (ModelState.IsValid)
            {
                var response = _stockService.GetByRemoteId(vm.Id);
                if (response.Stock == null)
                {
                    return RedirectToAction("Sell", new { vm.Id });
                }
                var userId = User.Identity.GetUserId();
                var user = await _userRepository.GetUserByIdIncludeStocksAsync(userId);
                if (user.Stocks.Any(s => s.RemoteId == vm.Id))
                {
                    var stock = user.Stocks.FirstOrDefault(s => s.RemoteId == vm.Id);
                    
                    if (stock != null)
                    {
                        if (stock.Count < vm.Count)
                        {
                            ModelState.AddModelError("", "You don't have enough of stocks");
                            return View(vm);
                        }
                        stock.Count -= vm.Count;
                        if (stock.Count <= 0)
                        {
                            user.Stocks.Remove(stock);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction("MyStocks");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You dont have any this stock");
                    return View();
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("MyStocks");
            }

            return View(vm);
        }

        private int GetCountByStockCode(List<Stock> stokcs, int code)
        {
            var stock = stokcs.FirstOrDefault(s => s.RemoteId == code);
            return stock?.Count ?? 0;
        }

        public async Task<ActionResult> MyStocks(int page = 1, int size = 15)
        {
            var userId = User.Identity.GetUserId();
            var user = await _userRepository.GetUserByIdIncludeStocksAsync(userId);
            var stockIds = user.Stocks.Select(s => s.RemoteId);
            var response = _stockService.GetByRemoteIds(stockIds.ToList(), page, size);

            var listItems =
                response.Stocks.Select(
                    s =>
                        new MyStockListItem()
                        {
                            Id = s.Id,
                            Price = s.Price,
                            Count = GetCountByStockCode(user.Stocks.ToList(), s.Id)
                        });

            var vm = new MyStocksViewModel()
            {
                Stocks = new StaticPagedList<MyStockListItem>(listItems, page, size, response.Count),
                UserName = user.UserName
            };

            return View(vm);
        }

        [AllowAnonymous]
        public ActionResult Refresh(int page, int size)
        {
            var stocks = _stockService.GetAll(page, size);
            var listItems = stocks.Stocks.Select(s => new StockListItem() { Id = s.Id, Price = s.Price });
            var vm = new StaticPagedList<StockListItem>(listItems, page, size, stocks.Count);
            return PartialView(vm);
        }


        public ActionResult RefreshMyStocks(int page, int size)
        {
            var userId = User.Identity.GetUserId();
            var user = AsyncHelper.RunSync(() => _userRepository.GetUserByIdIncludeStocksAsync(userId));
            var stockIds = user.Stocks.Select(s => s.RemoteId);
            var response = _stockService.GetByRemoteIds(stockIds.ToList(), page, size);

            var listItems =
                response.Stocks.Select(
                    s =>
                        new MyStockListItem()
                        {
                            Id = s.Id,
                            Price = s.Price,
                            Count = GetCountByStockCode(user.Stocks.ToList(), s.Id)
                        });

            var vm = new MyStocksViewModel()
            {
                Stocks = new StaticPagedList<MyStockListItem>(listItems, page, size, response.Count),
                UserName = user.UserName
            };

            return PartialView(vm);
        }
    }
}