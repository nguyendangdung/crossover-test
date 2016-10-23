using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class BuyStock
    {
        [Display(Name = "Code")]
        public int Id { get; set; }
        public decimal Price { get; set; }


        [Range(1, 1000000)]
        public int Count { get; set; }
    }
}