using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.PagedList;

namespace Client.Models
{
    public class MyStocksViewModel
    {
        public string UserName { get; set; }
        public StaticPagedList<MyStockListItem> Stocks { get; set; }
    }
}