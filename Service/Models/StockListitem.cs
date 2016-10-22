﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class StockListitem
    {
        [Display(Name = "Code")]
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}