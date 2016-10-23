using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Common;
using Ninject;
using Ninject.Web;
using Service.Models;
using Service.Repositories;

namespace Service
{
    /// <summary>
    /// Summary description for StockExchangeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StockExchangeService : WebServiceBase
    {
        [Inject]
        public IStockRepository StockRepository { get; set; }
        [WebMethod]
        public GetAllResponseMessage GetAll(int page = 1, int size = 15)
        {
            var stocks = StockRepository.GetAll().OrderBy(s => s.Id).Pager(page, size);
            var count = StockRepository.GetAll().Count();

            return new GetAllResponseMessage()
            {
                Count = count,
                Page = page,
                Size = size,
                Stocks = stocks.Select(s => new StockDetails() {Id = s.Id, Price = s.Price}).ToList()
            };
        }

        [WebMethod]
        public GetByIdsResponseMessage GetByIds(List<int> ids, int page = 1, int size = 15)
        {
            var stocks = StockRepository.GetByIds(ids).OrderBy(s => s.Id).Pager(page, size);
            var count = StockRepository.GetByIds(ids).Count();

            return new GetByIdsResponseMessage()
            {
                Count = count,
                Page = page,
                Size = size,
                Stocks = stocks.Select(s => new StockDetails() { Id = s.Id, Price = s.Price }).ToList()
            };
        }


        [WebMethod]
        public GetByIdResponseMessage GetById(int id)
        {
            var stock = StockRepository.GetById(id);
            return new GetByIdResponseMessage()
            {
                Stock = new StockDetails() { Id = stock.Id, Price = stock.Price}
            };
        }
    }
}
