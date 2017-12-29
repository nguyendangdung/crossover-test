using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class StockListitem
    {
        [Display(Name = "Code")]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool? Closed { get; set; }
    }
}