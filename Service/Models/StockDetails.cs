using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class StockDetails
    {
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Range(0, 2000)]
        public decimal Price { get; set; }
    }
}