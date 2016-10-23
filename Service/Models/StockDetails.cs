using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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