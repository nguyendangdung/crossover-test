using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web.Services;
using System.Web.Services.Protocols;
using Autofac;
using Common;
using Service.Entities;
using Service.Models;
using Service.Repositories;

namespace Service
{
    public class Authentication : SoapHeader
    {
        public string User;
        public string Password;
    }
    /// <summary>
    /// Summary description for StockExchangeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StockExchangeService : WebService
    {
        public Authentication Authentication { get; set; }

        private readonly IStockRepository _stockRepository;

        public StockExchangeService()
        {
            _stockRepository = new StockRepository(new Context());
        }

        [WebMethod]
        [SoapHeader("Authentication")]
        public GetAllResponseMessage GetAll(int page = 1, int size = 15)
        {
            if (!CheckAuthenticationHeader())
            {
                throw new AuthenticationException("Invalid credential");
            }
            var stocks = _stockRepository.GetAll().OrderBy(s => s.Id).Pager(page, size);
            var count = _stockRepository.GetAll().Count();

            return new GetAllResponseMessage()
            {
                Count = count,
                Page = page,
                Size = size,
                Stocks = stocks.Select(s => new StockDetails() {Id = s.Id, Price = s.Price}).ToList()
            };
        }

        [WebMethod]
        [SoapHeader("Authentication")]
        public GetByIdsResponseMessage GetByIds(List<int> ids, int page = 1, int size = 15)
        {
            if (!CheckAuthenticationHeader())
            {
                throw new AuthenticationException("Invalid credential");
            }

            var stocks = _stockRepository.GetByIds(ids).OrderBy(s => s.Id).Pager(page, size);
            var count = _stockRepository.GetByIds(ids).Count();

            return new GetByIdsResponseMessage()
            {
                Count = count,
                Page = page,
                Size = size,
                Stocks = stocks.Select(s => new StockDetails() { Id = s.Id, Price = s.Price }).ToList()
            };
        }


        [WebMethod]
        [SoapHeader("Authentication")]
        public GetByIdResponseMessage GetById(int id)
        {
            if (!CheckAuthenticationHeader())
            {
                throw new AuthenticationException("Invalid credential");
            }

            var stock = _stockRepository.GetById(id);
            return new GetByIdResponseMessage()
            {
                Stock = stock == null ? null : new StockDetails() { Id = stock.Id, Price = stock.Price}
            };
        }


        bool CheckAuthenticationHeader()
        {
            return Authentication?.User == "testuser" && Authentication?.Password == "testpassword";
        }
    }
}
