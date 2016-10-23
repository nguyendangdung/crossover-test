using System.Collections.Generic;

namespace Service.Models
{
    public class GetAllResponseMessage
    {
        public List<StockDetails> Stocks { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
    }
}