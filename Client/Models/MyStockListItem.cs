﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class MyStockListItem
    {
        [Display(Name = "Code")]
        public int Id { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Number Of Stocks")]
        public int Count { get; set; }
    }
}