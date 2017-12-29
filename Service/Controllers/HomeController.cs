using System.Linq;
using System.Web.Mvc;
using Common;
using Service.Entities;
using Service.Models;
using Service.Repositories;
using X.PagedList;

namespace Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContext _context;
        private readonly IStockRepository _stockRepository;

        public HomeController(IContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        public ActionResult Index(int page = 1, int size = 15)
        {
            var count = _stockRepository.GetAll().Count();
            var stocks = _stockRepository.GetAll().OrderBy(s => s.Id).Pager(page, size).ToList();
            var vm = stocks.Select(s => new StockListitem()
            {
                Id = s.Id,
                Price = s.Price,
                Closed = s.Closed.GetValueOrDefault()
            });
            return View(new StaticPagedList<StockListitem>(vm, page, size, count));
        }

        public ActionResult Edit(int id)
        {
            var stock = _stockRepository.GetById(id);
            if (stock == null)
            {
                return HttpNotFound();
            }

            return View(new StockDetails() {Id = stock.Id, Price = stock.Price, Closed = stock.Closed.GetValueOrDefault()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StockDetails vm)
        {
            if (ModelState.IsValid)
            {
                var stock = _stockRepository.GetById(vm.Id);
                if (stock == null)
                {
                    return RedirectToAction("Edit", new {vm.Id});
                }

                stock.Price = vm.Price;
                stock.Closed = vm.Closed;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}