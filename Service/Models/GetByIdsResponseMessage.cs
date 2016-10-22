using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class GetByIdsResponseMessage
    {
        public List<StockDetails> Stocks { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
    }
}